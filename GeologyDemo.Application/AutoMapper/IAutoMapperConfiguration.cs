﻿using System.Collections.Generic;

namespace GeologyDemo.Application.AutoMapper
{
    public interface IAutoMapperConfiguration
    {
        TReturn MapObject<TMap, TReturn>(TMap obj) where TMap : class where TReturn : class;
        List<TReturn> MapCollection<TMap, TReturn>(IEnumerable<TMap> expression) where TMap : class where TReturn : class;
    }
}