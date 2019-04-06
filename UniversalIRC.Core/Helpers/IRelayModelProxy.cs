using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalIRC.Core.Helpers
{
    public interface IRelayModelProxy<TModel>
        where TModel : class
    {
        TModel RelayModel { get; }
    }
}
