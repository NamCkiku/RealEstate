using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RealEstateMobile.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
        protected Page page;
        public ViewModelBase(Page page)
        {
            this.page = page;
        }
    }
}
