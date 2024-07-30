using BookStore.Data;
using BookStore.Interfaces;
using BookStore.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace BookStore.Services
{
    public class AddressService: IAddressService
    {
        public readonly IConfiguration _configuration;
        public AddressService(IConfiguration configuration) => _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        public List<Address> GetAllAddress()
        {
            return MockData.addresses;
        }

        public Address GetAddress(int addressId)
        {
            return MockData.addresses.FirstOrDefault(x => x.Id == addressId);
        }
    }
}
