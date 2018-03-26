using FluentValidation;
using Store.Repository;

namespace Store.BLL.Validation
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
