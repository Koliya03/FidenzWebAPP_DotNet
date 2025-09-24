using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.Services.IServices;
using FidenzApp.Application.Interfaces.UnitOfWork;
using FidenzApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FidenzApp.Application.Interfaces.Services.services
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private CustomerDto MapToDto(Customers c)
        {
          return  new CustomerDto
            {
                _id = c._id,
                index = c.index,
                age = c.age,
                eyeColor = c.eyeColor,
                name = c.name,
                gender = c.gender,
                company = c.company,
                email = c.email,
                phone = c.phone,
                about = c.about,
                registered = c.registered,
                latitude = c.latitude,
                longitude = c.longitude,
                tags = c.tags ?? new List<string>(),
                address = c.address == null ? null : new AddressDto
                {
                    number = c.address.number,
                    street = c.address.street,
                    city = c.address.city,
                    state = c.address.state,
                    zipcode = c.address.zipcode
                }
            }; 
        }

        private List<CustomerDto> MapToDto(IEnumerable<Customers> items)
        {
            var list = new List<CustomerDto>();
            if (items == null) return list;

            foreach (var c in items)
            {
                if (c != null)
                    list.Add(MapToDto(c));
            }
            return list;
        }
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customersList = await _unitOfWork.CustomerRepository.GetAllAsync();
            if (customersList.Count() > 0)
            {
                return MapToDto(customersList);

            }
            else
            {
                return new List<CustomerDto>();
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetAllBySearchAsync(string search)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();

            var customersListByDetails = customers.Where(c =>
                                                        c.name.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || c.email.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || c.phone.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || c.company.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || c.address.street.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || c.address.city.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || c.address.state.Contains(search, StringComparison.OrdinalIgnoreCase)
                                                    || c.address.zipcode.ToString().Contains(search)
                                                    || c.tags.Any(tag => tag.Contains(search, StringComparison.OrdinalIgnoreCase))
                                                    ).ToList();


            if (customersListByDetails.Count() > 0)
            { 
                return MapToDto(customersListByDetails);
            }
            else
            {
                return new List<CustomerDto>();
            }
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(string id)
        {
             var customer =  await _unitOfWork.CustomerRepository.GetSync(x => x._id == id);
            if (customer == null)
            {
                return null;
            }
            return MapToDto(customer);
           
        }

        public async Task<IEnumerable<CustomerDto>> GetAllByZipAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            var customersByZip = customers.Select(c => c)
                .OrderBy(x => x.address.zipcode).ToList();
            return MapToDto(customersByZip);
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
