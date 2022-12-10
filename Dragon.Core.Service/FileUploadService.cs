using AutoMapper;
using Dragon.Core.Common;
using Dragon.Core.Entity;
using Dragon.Core.Entity.Enum;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class FileUploadService :  BaseService<SysFileItem>, IFileUploadService
    {
        private readonly IUser _user;
        private readonly IBaseRepository<SysUser> _userRepository;
        private readonly IDepartMentRepository _departMentRepository;
        private readonly IMapper _mapper;
        public FileUploadService(IBaseRepository<SysFileItem> baseRepository,IUser user, IBaseRepository<SysUser> userRepository, IDepartMentRepository departMentRepository,IMapper mapper) : base(baseRepository)
        {
            _user = user;
            _userRepository = userRepository;
            _departMentRepository = departMentRepository;
            _mapper = mapper;
        }
        public async Task<PageModel<FileOutput>>GetPageListFileAsync(FilePageInput filePageInput)
        {
            string fileName = filePageInput.FileName;
            string dept = filePageInput.FileOwnDept;
            var query = _baseRepository.Table.Where(u => u.IsDrop == false).WhereIf(u => u.FileName.Contains(fileName), !string.IsNullOrWhiteSpace(fileName)).WhereIf(u => u.FileOwnDept.Contains(dept), !string.IsNullOrWhiteSpace(dept));
            int count = await query.CountAsync().ConfigureAwait(false);
            var pageQuery = await query.ToPageListAsync(filePageInput.Page, filePageInput.PageSize);
            var models = _mapper.Map<List<FileOutput>>(pageQuery);
            return new PageModel<FileOutput> (models,filePageInput.Page,filePageInput.PageSize,count);
        }
        public async Task<string> UploadFile(IFormFile file,string path)
        {
            string savePath = $"{path}/Upload/";
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string saveFileName = Path.Combine(savePath,file.FileName);
            using (var stream = File.Create(saveFileName))
            {
                await file.CopyToAsync(stream);
            }
            return file.FileName;
        }

        public async Task<bool> AddFileDataAsync(SysFileItem sysFileItem)
        {
            sysFileItem.CreatedName = _user.Name;
            sysFileItem.UpdateName= _user.Name;
            sysFileItem.CreatedTime=DateTime.Now;
            sysFileItem.UpdateTime= DateTime.Now;
            string userid = _user.ID;
            var sysUser =await _userRepository.GetEntityAsync(Convert.ToInt32(_user.ID));
            var sysDept = await _departMentRepository.GetEntityAsync(sysUser!.DepartmentId);
            sysFileItem.FileOwnDept = sysDept?.Name;
            sysFileItem.DeptId = sysDept?.Id ?? 0;
            await InsertAsync(sysFileItem);
            return true;
        }

        public async Task<bool> UpdateFileAsync(BaseFileDto baseFileDto,int id)
        {
            baseFileDto.UpdateName = _user.Name;
            baseFileDto.updateTime = DateTime.Now;
            var sysUser = await _userRepository.FindAsync(d=>d.Id== _user.ID.ObjToInt());
            var sysDept = await _departMentRepository.GetEntityAsync(sysUser!.DepartmentId);
            baseFileDto.FileOwnDept = sysDept?.Name;
            baseFileDto.DeptId = sysDept?.Id ?? 0;
            int num =await _baseRepository.UpdateNotQueryAsync<BaseFileDto>(d => d.Id == id, baseFileDto);
            return num > 0;
        }

        public async Task<bool> UpdateFileAsync(int id,FileDto fileDto)
        {
            fileDto.UpdateName = _user.Name;
            fileDto.updateTime = DateTime.Now;
            var sysUser = await _userRepository.FindAsync(d => d.Id == _user.ID.ObjToInt());
            var sysDept = await _departMentRepository.GetEntityAsync(sysUser!.DepartmentId);
            fileDto.FileOwnDept = sysDept?.Name;
            fileDto.DeptId = sysDept?.Id ?? 0;
            int num = await _baseRepository.UpdateNotQueryAsync<BaseFileDto>(d => d.Id == id, fileDto);
            return num > 0;
        }
    }
}
