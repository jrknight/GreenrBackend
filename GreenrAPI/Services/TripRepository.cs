using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenrAPI.Entities;

namespace GreenrAPI.Services
{
    public class TripRepository : ITripRepository
    {
        public Task AddTripAsync(Trip item)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrip(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Trip> GetTripAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Trip>> GetTripsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> TripExistsAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTripAsync(Trip item)
        {
            throw new NotImplementedException();
        }
    }
}
