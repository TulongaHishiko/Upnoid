using System;
using System.Collections.Generic;
using System.Text;
using Upnoid.Domain.Models;

namespace Upnoid.Core.Abstracts
{
    public interface ICustomerRepository : IBaseRepository<Customer> { }
    public interface IMovieRepository : IBaseRepository<Movie> { }
    public interface IRentalRepository : IBaseRepository<Rental> { }
    public interface IGenreRepository : IBaseRepository<Genre> { }
    public interface ITrailerRepository : IBaseRepository<Trailer> { }
}
