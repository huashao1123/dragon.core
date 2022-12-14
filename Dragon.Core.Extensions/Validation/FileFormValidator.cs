using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dragon.Core.ViewModel;
using FluentValidation;

namespace Dragon.Core.Extensions
{
    public class FileFormValidator: AbstractValidator<FileFormDto>
    {
        public FileFormValidator()
        {
            RuleFor(d=>d.fileVersion).NotEmpty().Length(3,10).WithMessage("文件版本长度为3到10");
        }
    }
}
