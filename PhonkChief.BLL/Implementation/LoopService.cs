using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhonkChief.BLL.Interfaces;
using PhonkChief.DAL.Data;
using PhonkChief.Domain.DTO;
using PhonkChief.Domain.Entities;
using PhonkChief.Domain.Enum;
using PhonkChief.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhonkChief.BLL.Implementation
{
    public class LoopService : ILoopService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public LoopService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<BaseResponse<Loop>> CreateAsync(Loop loop)
        {
            var response = new BaseResponse<Loop>();

            try
            {
                await _db.Loops.AddAsync(loop);
                await _db.SaveChangesAsync();
                response.Message = $"Объект создан";
                response.Data = loop;
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Loop> { Message = $"Ошибка создания объекта: {ex.Message}" };
            }
        }
        public async Task<BaseResponse<LoopsViewModel>> GetAllAsync()
        {
            var response = new BaseResponse<LoopsViewModel>();
            var modelList = new List<LoopsDto>();
            var dto = new LoopsDto();
            try
            {
                var loops = await _db.Loops.Include(u => u.User).ToListAsync();

                foreach (var loop in loops)
                {
                    var item = _mapper.Map<LoopsDto>(loop);
                    item.Avatar = loop.User.Avatar;
                    item.NickName = loop.User.NickName;
                    modelList.Add(item);
                }
                int numOfPages = (int)Math.Ceiling(modelList.Count / 2.0);
                var model = new LoopsViewModel { LoopsList = modelList, NumOfPage = numOfPages };
                response.Data = model;
                response.Message = $"Объекты получены";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<LoopsViewModel> { Message = $"Ошибка запроса: {ex.Message}" };
            }
        }

        public async Task<BaseResponse<LoopsViewModel>> GetByCategoryAsync(Category category)
        {
            var response = new BaseResponse<LoopsViewModel>();
            var modelList = new List<LoopsDto>();
            var dto = new LoopsDto();
            try
            {
                var loops = await _db.Loops.Include(u => u.User).ToListAsync();

                foreach (var loop in loops)
                {
                    if(loop.Category == category )
                    {
                        var item = _mapper.Map<LoopsDto>(loop);
                        item.Avatar = loop.User.Avatar;
                        item.NickName = loop.User.NickName;
                        modelList.Add(item);
                    }
                }
                int numOfPages = (int)Math.Ceiling(modelList.Count / 2.0);
                var model = new LoopsViewModel { LoopsList = modelList, NumOfPage = numOfPages };
                response.Data = model;
                response.Message = $"Объекты получены";
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse<LoopsViewModel> { Message = $"Ошибка запроса: {ex.Message}" };
            }
        }
    }
}

