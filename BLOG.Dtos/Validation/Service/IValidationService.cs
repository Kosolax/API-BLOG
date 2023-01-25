namespace BLOG.Dtos.Validation.Service
{
    using System.Collections.Generic;

    public interface IValidationService<T>
    {
        bool IsValid { get; set; }

        Dictionary<string, string> ModelState { get; set; }

        void AddError(string key, string errorMessage);

        void Clear();

        bool Validate(T itemToValidate);
    }
}