using FidenzApp.Application.DTO;
using FidenzApp.Application.Interfaces.Services.IServices;
using FidenzApp.Application.Interfaces.UnitOfWork;
using FidenzApp.Domain.Entities;
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
        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            var customersList = await _unitOfWork.CustomerRepository.GetAllAsync();
            if (customersList.Count() > 0)
            {
                return customersList;

            }
            else
            {
                return Enumerable.Empty<Customers>().ToList();
            }
        }

        public async Task<IEnumerable<Customers>> GetAllBySearchAsync(string search)
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
            { return customersListByDetails; }
            else
            {
                return Enumerable.Empty<Customers>().ToList();
            }
        }

        public async Task<Customers?> GetCustomerByIdAsync(string id)
        {
             return await _unitOfWork.CustomerRepository.GetSync(x => x._id == id);
           
        }

        public async Task<IEnumerable< Customers>> GetAllByZipAsync()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            var customersByZip = customers.Select(c => c)
                .OrderBy(x => x.address.zipcode).ToList();
            return customersByZip;
        }

        public async Task<double> getDistance(string id, double latitude, double longitude)
        {
            Customers customer = await _unitOfWork.CustomerRepository.GetSync(x => x._id == id);
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

            customer.name = updateCustomer.Name;
            customer.email = updateCustomer.Email;
            customer.phone = updateCustomer.Phone;

            await _unitOfWork.CustomerRepository.saveAsync();
        }


    }
}
