using FluentValidation;
using Store.Repository;

namespace Store.Service.Validation
{
    public class BaseValidator<T>: AbstractValidator<T>
    {
        private RepositoryPlaceholder _repo = new RepositoryPlaceholder();

        public RepositoryPlaceholder Repo {
            get
            {
                return _repo;
            }
        }
    }
}
