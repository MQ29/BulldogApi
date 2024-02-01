using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task Create(Company company)
        {
            await _companyRepository.AddAsync(company);
        }

        public async Task Update(Guid companyId, Address address, string name, string phoneNumber)
        {
            try
            {
                var companyDto = await GetCompanyAsync(companyId);
                companyDto.Address = address;
                companyDto.Name = name;
                companyDto.PhoneNumber = phoneNumber;
                var company = _mapper.Map<Company>(companyDto);
                await _companyRepository.Update(company);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid companyId)
        {
            var company = await _companyRepository.GetAsync(companyId);
            if (company is null)
            {
                throw new Exception($"Company with Id {companyId} wasnt found.");
            }
            var mappedCompany = _mapper.Map<CompanyDto>(company);
            return mappedCompany;
        }

        public async Task<CompanyDto> GetByUserIdAsync(string userId)
        {
            var company = await _companyRepository.GetByUserIdAsync(userId);
            if (company is null)
            {
                throw new Exception($"Company with userId {userId} wasnt found.");
            }
            var mappedCompany = _mapper.Map<CompanyDto>(company);
            return mappedCompany;
        }
    }
}

