using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WeatherForecast.CSharp.Domain;

namespace WeatherForecast.CSharp.Storage
{
    public class ForecastStorageService : IStorageService<Forecast, string>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public ForecastStorageService(AppDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }
        
        public async Task<Forecast> GetAsync(string key)
        {
            return await _dbContext.Forecasts
                .Include(_dbContext.GetIncludePaths<ForecastEntity>())
                .ProjectTo<Forecast>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(f => f.City == key);
        }

        public async Task<Forecast> SaveAsync(Forecast obj)
        {
            if (await _dbContext.Forecasts.AnyAsync(f => f.Id == obj.Id))
            {
                var entity = await _dbContext.Forecasts
                    .Include(_dbContext.GetIncludePaths<ForecastEntity>())
                    .SingleOrDefaultAsync(f => f.Id == obj.Id);

                _dbContext.ForecastItems.RemoveRange(entity.Items);

                entity.Items = _mapper.Map<IEnumerable<ForecastItemEntity>>(obj.Items);
                
                entity.Created = DateTimeOffset.Now;
                
                await _dbContext.SaveChangesAsync();
                
                return _mapper.Map<Forecast>(entity);
            }
            else
            {
                var entity = _mapper.Map<ForecastEntity>(obj);
                
                entity.Created = DateTimeOffset.Now;
                
                _dbContext.Forecasts.Add(entity);
                
                await _dbContext.SaveChangesAsync();
                
                return _mapper.Map<Forecast>(entity);
            }
        }
    }
}