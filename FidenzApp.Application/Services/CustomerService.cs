using AutoMapper;
using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.Services;
using FidenzApp.Application.Interfaces.UnitOfWork;
using FidenzApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
           
        

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customersList = await _unitOfWork.CustomerRepository.GetAllAsync();
            if (customersList.Count() > 0)
            {
                return _mapper.Map<IEnumerable<CustomerDto>>(customersList);

            }
            else
            {
                return new List<CustomerDto>();
            }
        }

        //public async Task<IEnumerable<CustomerDto>> GetAllBySearchAsync(string search)
        //{
        //    var customersList = await _unitOfWork.CustomerRepository.GetAllAsync(c =>
        //                                                c.name.Contains(search, StringComparison.OrdinalIgnoreCase)
        //                                            || c.email.Contains(search, StringComparison.OrdinalIgnoreCase)
        //                                            || c.phone.Contains(search, StringComparison.OrdinalIgnoreCase)
        //                                            || c.company.Contains(search, StringComparison.OrdinalIgnoreCase)
        //                                            || c.address.street.Contains(search, StringComparison.OrdinalIgnoreCase)
        //                                            || c.address.city.Contains(search, StringComparison.OrdinalIgnoreCase)
        //                                            || c.address.state.Contains(search, StringComparison.OrdinalIgnoreCase)
        //                                            || c.address.zipcode.ToString().Contains(search)
        //                                            || c.tags.Any(tag => tag.Contains(search, StringComparison.OrdinalIgnoreCase))


        //        );

        //    if (customersList.Count() > 0)
        //    {
        //        return _mapper.Map<IEnumerable<CustomerDto>>(customersList);

        //    }
        //    else
        //    {
        //        return new List<CustomerDto>();
        //    }
        //}


        public async Task<IEnumerable<CustomerDto>> GetAllBySearchAsync(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return Array.Empty<CustomerDto>();

            var like = $"%{search.Trim()}%";

            var customersList = await _unitOfWork.CustomerRepository.GetAllAsync(c =>
                   EF.Functions.Like(c.name, like) ||
                   EF.Functions.Like(c.email, like) ||
                   EF.Functions.Like(c.phone, like) ||
                   EF.Functions.Like(c.company, like) ||
                   EF.Functions.Like(c.address.street, like) ||
                   EF.Functions.Like(c.address.city, like) ||
                   EF.Functions.Like(c.address.state, like) ||
                   EF.Functions.Like(c.address.zipcode.ToString(), like) ||
                   c.tags.Any(tag => EF.Functions.Like(tag,like))
                   );

            if (!customersList.Any())
                return Array.Empty<CustomerDto>();

            return _mapper.Map<IEnumerable<CustomerDto>>(customersList);
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(string id)
        {
             var customer =  await _unitOfWork.CustomerRepository.GetSync(x => x._id == id);
            if (customer == null)
            {
                return null;
            }
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<ZipGroupDto>> GetAllByZipAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllByZipAsync();
            return customers;
        }

        public async Task<ActionResult<double>> getDistance(string id, double latitude, double longitude)
        {
            var customer = await _unitOfWork.CustomerRepository.GetSync(x => x._id == id);
            if (customer is null)
                return null;

            double customerLongitude = customer.longitude;
            double customerLatitude = customer.latitude;

            const double R = 6371.0088;
            const double p = Math.PI / 180;

            double dLat = (latitude - customerLatitude) * p;
            double dLon = (longitude - customerLongitude) * p;

            double a = 0.5 - Math.Cos(dLat) / 2
                        + Math.Cos(latitude * p) * Math.Cos(customerLatitude * p) * (1 - Math.Cos(dLon)) / 2;

            a = Math.Max(0.0, Math.Min(1.0, a));
            double km = 2 * R * Math.Asin(Math.Sqrt(a));

            return km;
        }

        public async Task UpdateCustomer(string id, updateCustomerDto updateCustomer)
        {
            var customer = await _unitOfWork.CustomerRepository.GetSync(u => u._id == id);
            if (updateCustomer.Name is not null)
                customer.name = updateCustomer.Name;

            if (updateCustomer.Email is not null)
                customer.email = updateCustomer.Email;

            if (updateCustomer.Phone is not null)
                customer.phone = updateCustomer.Phone;
           
            await _unitOfWork.CustomerRepository.saveAsync();
        }

        
    }
}
