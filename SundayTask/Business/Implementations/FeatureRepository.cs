using Business.Services;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class FeatureRepository : IFeatureService
    {
        private readonly AppDbContext _context;
        public FeatureRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Feature> Get(int? id)
        {
            if(id is null)
            {
                throw new ArgumentNullException();
            }
            Feature feature = await _context.Features.Where(n=>n.Id ==id).Where(n=>!n.IsDeleted).FirstOrDefaultAsync();
            if(feature is null)
            {
                throw new NullReferenceException();
            }
            return feature;
        }

        public async Task<List<Feature>> GetAll()
        {
            List<Feature> features = await _context.Features.Where(n => !n.IsDeleted).ToListAsync();
            if(features is null)
            {
                throw new NullReferenceException();
            }
            return features;
        }

        public async Task Create(Feature feature)
        {
            if (feature is null)
            {
                throw new ArgumentNullException();
            }
            feature.CreatedDate = DateTime.Now;
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            Feature feature = await Get(id);
            feature.IsDeleted = true;
            _context.Features.Update(feature);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Feature feature)
        {
            if(feature is null)
            {
                throw new ArgumentNullException();
            }
            feature.UpdatedDate = DateTime.Now;
            _context.Features.Update(feature);
            await _context.SaveChangesAsync();
        }
    }
}
