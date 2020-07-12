using InsuranceSolution.API.Models;
using InsuranceSolution.API.Data.Contexts;
using InsuranceSolution.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceSolution.API.Controllers
{
    [Route("/api/contacts")]
    [Produces("application/json")]
    [ApiController]
    public class ContactsController : Controller
    { 
        private readonly AppDbContext _appDbContext;

        public ContactsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Saves a new Contact.
        /// </summary>
        /// <param name="contact">Contact data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Contact), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public IActionResult Post([FromBody] Contact contact)
        {
            contact.RateChart = RateChart(contact);
            _appDbContext.Contacts.Add(contact);
             
            if (_appDbContext.SaveChanges(true)==0)
            {
                return BadRequest(new ErrorResource("Error while saving contact"));
            } 
            return Ok(contact);
        }

        /// <summary>
        /// Updates an existing contact according to an identifier.
        /// </summary>
        /// <param name="id">Contact identifier.</param>
        /// <param name="contact">Updated contact data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Contact), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public  IActionResult Put(int id, [FromBody] Contact contact)
        {
            var existingContact = _appDbContext.Contacts.FirstOrDefault(c => c.Id == id);

            if (existingContact == null)
                return NotFound(new ErrorResource("Contact not found"));

            existingContact.RateChart = RateChart(contact);
            existingContact.RateChartId = contact.RateChart == null ? 0 : contact.RateChart.Id;

            existingContact.Name = contact.Name;
            existingContact.Gender = contact.Gender;
            existingContact.Address = contact.Address;
            existingContact.City = contact.City;
            existingContact.Country = contact.Country;
            existingContact.DOB = contact.DOB;
            existingContact.SaleDate = contact.SaleDate;
            existingContact.Sate = contact.Sate;

            try
            {
                _appDbContext.Update(existingContact);
                _appDbContext.SaveChanges(true);
               
                existingContact.RateChart.Contacts = null;
                existingContact.RateChart.CoveragePlan.RateCharts = null;
                return Ok(existingContact);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return BadRequest($"An error occurred when updating the Contact: {ex.Message}");
            } 
        }


        /// <summary>
        /// Deletes a given Contact  according to an identifier.
        /// </summary>
        /// <param name="id">Contact identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Contact), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public    IActionResult Delete(int id)
        {
            var result = _appDbContext.Contacts.FirstOrDefault(c => c.Id == id);

            if (result==null)
            {
                return BadRequest(new ErrorResource("Contact not found"));
            }
            _appDbContext.Remove(result);
            _appDbContext.SaveChanges(true);
            return Ok(result);
        }

        /// <summary>
        /// Lists all Contacts.
        /// </summary>
        /// <returns>List of Contacts.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Contact>), 200)]
        public    IEnumerable<Contact> List()
        {
            return _appDbContext.Contacts.Include(c => c.RateChart).Include(c => c.RateChart.CoveragePlan).ToList();
        }
        private RateChart RateChart(Contact contact)
        {
            CoveragePlan coveragePlan = _appDbContext.CoveragePlans.Include(c=>c.RateCharts).FirstOrDefault(c=>c.EligibilityCountry.ToLower()==contact.Country.ToLower());
            if (coveragePlan == null)
            {
                coveragePlan = _appDbContext.CoveragePlans.Include(c => c.RateCharts).FirstOrDefault(c => c.IsDefault);
            }
            // Age on Sales Date
            List<RateChart> filterByCountry = coveragePlan.RateCharts.ToList();
            int age = contact.SaleDate.Year - contact.DOB.Year;
            List<RateChart> filteredByGender = filterByCountry.Where(r => r.Gender == contact.Gender).ToList();
            RateChart filterbyAge = filteredByGender.FirstOrDefault(r => age > r.AgeAbove) ?? filteredByGender.FirstOrDefault(r => age < r.AgeBelow);

            return filterbyAge;
        }
    }
}
