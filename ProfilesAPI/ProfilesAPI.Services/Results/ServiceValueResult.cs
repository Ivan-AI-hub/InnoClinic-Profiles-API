﻿using ProfilesAPI.Application.Results;

namespace ProfilesAPI.Services.Results
{
    public class ServiceValueResult<T> : IServiceResult
        where T : class
    {
        public IList<string> Errors { get; }
        public T? Value { get; internal set; }
        public bool IsComplite => Errors.Count == 0;

        public ServiceValueResult(T? value = default, params string[] errors)
        {
            Value = value;
            Errors = errors.ToList();
        }

        public ServiceValueResult(IApplicationResult applicationResult)
        {
            Errors = applicationResult.Errors;
        }
        public ServiceValueResult(IServiceResult serviceResult)
        {
            Errors = serviceResult.Errors;
        }
        public ServiceValueResult(ApplicationValueResult<T> applicationResult)
        {
            Value = applicationResult.Value;
            Errors = applicationResult.Errors;
        }
    }
}
