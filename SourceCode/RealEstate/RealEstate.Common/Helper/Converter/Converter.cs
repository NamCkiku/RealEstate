using System;

namespace RealEstate.Common.Helper
{
    public abstract class Converter
    {
        // Fields
        private readonly Guid _From;
        private readonly Guid _To;

        // Properties
        public Guid From { get { return _From; } }
        public Guid To { get { return _To; } }

        // Constructors
        internal Converter(Guid from, Guid to)
        {
            _From = from;
            _To = to;
        }

        // Functions
        public abstract object Convert(object obj);
    }
}
