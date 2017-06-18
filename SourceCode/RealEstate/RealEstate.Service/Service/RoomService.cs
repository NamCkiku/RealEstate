using RealEstate.Entities.Entites;
using RealEstate.Repository.Infrastructure;
using RealEstate.Service.BaseService;
using RealEstate.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Entities.ModelView;
using RealEstate.Repository.IRepositories;
using RealEstate.Common.Helper;
using RealEstate.Common.Constants;

namespace RealEstate.Service.Service
{
    public class RoomService : BaseService<Room>, IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRoomTagRepository _roomTagRepository;
        public RoomService(IRoomRepository roomRepository,
            IRepository<Room> repository,
            ITagRepository tagRepository,
            IRoomTagRepository roomTagRepository,
            IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
            this._roomRepository = roomRepository;
            this._tagRepository = tagRepository;
            this._roomTagRepository = roomTagRepository;
        }

        public IEnumerable<Room> GetAllListRoom(int top)
        {
            List<Room> lstroom = new List<Room>();
            try
            {
                lstroom = _roomRepository.GetMulti(x => x.isDelete == false && x.Status == true).Take(top).ToList();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllListRoom('{0}')", "");
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
            }
            return lstroom;
        }

        public IEnumerable<Room> GetAllListRoomByUser(string userID, int page, int pageSize, out int totalRow)
        {
            List<Room> lstroom = new List<Room>();
            try
            {
                lstroom = _roomRepository.GetMulti(x => x.UserID == userID && x.isDelete == false).ToList();
                totalRow = lstroom.Count();
                if (lstroom != null)
                {
                    lstroom.Skip(page * pageSize).Take(pageSize);
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllListRoomByUser('{0}')", "");
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
                totalRow = 0;
            }
            return lstroom;
        }

        public IEnumerable<Room> GetAllListRoomPaging(SearchRoomEntity filter, int page, int pageSize, out int totalRow)
        {
            DateTime st = filter.StartDate == null ? DateTime.MinValue : filter.StartDate.Value.Date;
            DateTime et = filter.EndDate == null ? DateTime.MaxValue : filter.EndDate.Value.Date.AddDays(1);
            if (filter.Keywords == null)
            {
                filter.Keywords = "";
            }
            List<Room> lstroom = new List<Room>();
            try
            {
                lstroom = _roomRepository.GetMulti(x => x.Status == filter.Status
                && (filter.RoomTypeID == null || x.RoomTypeID == filter.RoomTypeID)
                && (filter.ProvinceID == null || x.ProvinceID == filter.ProvinceID)
                && (filter.DistrictID == null || x.DistrictID == filter.DistrictID)
                && (filter.WardID == null || x.WardID == filter.WardID)
                && (filter.StartDate == null || x.CreatedDate >= st || x.CreatedDate == null)
                && (filter.EndDate == null || x.CreatedDate < et || x.CreatedDate == null)
                && (x.RoomName.Contains(filter.Keywords) || x.Description.Contains(filter.Keywords) || string.IsNullOrEmpty(filter.Keywords))
                ).OrderByDescending(x => x.DisplayOrder).ToList();
                totalRow = lstroom.Count();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllListRoomPaging('{0}')", lstroom);
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
                totalRow = 0;
                return null;
            }
            return lstroom.Skip((page) * pageSize).Take(pageSize);
        }

        public IEnumerable<Room> GetAllListRoomFullSearch(SearchRoomEntity filter, int page, int pageSize, out int totalRow, string sort)
        {
            List<Room> lstroom = new List<Room>();
            if (string.IsNullOrEmpty(filter.Keywords))
            {
                filter.Keywords = "";
            }
            try
            {
                lstroom = _roomRepository.GetMulti(x => x.Status == true
                && (x.RoomTypeID == filter.RoomTypeID || filter.RoomTypeID == null)
                && (x.Price >= filter.PriceFrom || filter.PriceFrom == null)
                && (x.Price <= filter.PriceTo || filter.PriceTo == null)
                && (x.ProvinceID == filter.ProvinceID || filter.ProvinceID == null)
                && (x.DistrictID == filter.DistrictID || filter.DistrictID == null)
                && (x.WardID == filter.WardID || filter.WardID == null)
                && (x.RoomName.Contains(filter.Keywords) || string.IsNullOrEmpty(filter.Keywords))
                ).ToList();

                switch (sort)
                {
                    case "ViewCount":
                        lstroom = lstroom.OrderByDescending(x => x.ViewCount).ToList();
                        break;

                    case "DisplayOrder":
                        lstroom = lstroom.OrderByDescending(x => x.DisplayOrder.HasValue).ToList();
                        break;

                    case "Price":
                        lstroom = lstroom.OrderBy(x => x.Price).ToList();
                        break;

                    default:
                        lstroom = lstroom.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                }
                totalRow = lstroom.Count();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllListRoomFullSearch('{0}')", lstroom);
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
                totalRow = 0;
                return null;
            }
            return lstroom.Skip((page) * pageSize).Take(pageSize);
        }

        public IEnumerable<Room> GetAllListRoomVip(int top, int vipID)
        {
            List<Room> lstroom = new List<Room>();
            try
            {
                lstroom = _roomRepository.GetMulti(x => x.isDelete == false
                && x.Status == true && x.VipID == vipID).Take(top).ToList();
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("GetAllListRoomVip('{0}')", "");
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
            }
            return lstroom;
        }

        public void IncreaseView(int id)
        {
            var product = _roomRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public Room InsertRoom(Room room)
        {
            Room roomResult = new Room();
            try
            {
                roomResult = _roomRepository.Add(room);
                _unitOfWork.Commit();
                if (!string.IsNullOrEmpty(room.Tags))
                {
                    string[] tags = room.Tags.Split(',');
                    for (var i = 0; i < tags.Length; i++)
                    {
                        var tagId = StringHelper.ToUnsignString(tags[i]);
                        if (_tagRepository.Count(x => x.ID == tagId) == 0)
                        {
                            Tag tag = new Tag();
                            tag.ID = tagId;
                            tag.Name = tags[i];
                            tag.Type = CommonConstants.RoomTag;
                            _tagRepository.Add(tag);
                        }

                        RoomTag roomTag = new RoomTag();
                        roomTag.RoomID = room.ID;
                        roomTag.TagID = tagId;
                        _roomTagRepository.Add(roomTag);
                    }
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("InsertRoom('{0}')", "");
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
            }

            return roomResult;
        }

        public void UpdateRoom(Room room)
        {
            try
            {
                _roomRepository.Update(room);
                if (!string.IsNullOrEmpty(room.Tags))
                {
                    string[] tags = room.Tags.Split(',');
                    for (var i = 0; i < tags.Length; i++)
                    {
                        var tagId = StringHelper.ToUnsignString(tags[i]);
                        if (_tagRepository.Count(x => x.ID == tagId) == 0)
                        {
                            Tag tag = new Tag();
                            tag.ID = tagId;
                            tag.Name = tags[i];
                            tag.Type = CommonConstants.RoomTag;
                            _tagRepository.Add(tag);
                        }
                        _roomTagRepository.DeleteMulti(x => x.RoomID == room.ID);
                        RoomTag roomTag = new RoomTag();
                        roomTag.RoomID = room.ID;
                        roomTag.TagID = tagId;
                        _roomTagRepository.Add(roomTag);
                    }
                }
            }
            catch (Exception ex)
            {
                string FunctionName = string.Format("UpdateRoom('{0}')", "");
                Common.Logs.LogCommon.WriteLogError(ex.Message + FunctionName);
            }
        }
    }
}
