using System;
using System.Collections.Generic;

namespace RockApp.Models
{
    public interface IIdentifyable<TKey>
    {
        TKey Id { get; }
    }
}