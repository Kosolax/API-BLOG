namespace BLOG.Dtos.Validation.Service
{
    using System.Collections.Generic;

    public abstract class ValidationService<T> : IValidationService<T>
    {
        public ValidationService()
        {
            this.ModelState = new Dictionary<string, string>();
            this.IsValid = true;
        }

        public bool IsValid { get; set; }

        public Dictionary<string, string> ModelState { get; set; }

        public void AddError(string key, string errorMessage)
        {
            if (!this.ModelState.ContainsKey(key))
            {
                this.ModelState.Add(key, errorMessage);
            }
            else
            {
                this.ModelState[key] = errorMessage;
            }

            this.IsValid = this.ModelState.Count == 0;
        }

        public void Clear()
        {
            try
            {
                this.ModelState.Clear();
            }
            catch
            {
                this.ModelState = new Dictionary<string, string>();
            }

            this.IsValid = this.ModelState.Count == 0;
        }

        public abstract bool Validate(T itemToValidate);

        protected void ClearDictionary(bool clear)
        {
            if (clear)
            {
                this.Clear();
            }
        }
    }
}