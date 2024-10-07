using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileAPI.Data;
using ProfileAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserProfilesController(ApplicationDbContext context)
        {
            _context = context; // Inject the DbContext
        }

        [HttpGet] // GET: api/userprofiles
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            return await _context.UserProfiles.ToListAsync(); // Fetch all user profiles
        }

        [HttpGet("{id}")] // GET: api/userprofiles/{id}
        public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id); // Find profile by ID

            if (userProfile == null)
            {
                return NotFound(); // Return 404 if not found
            }

            return userProfile; // Return the found profile
        }

        [HttpPost] // POST: api/userprofiles
        public async Task<ActionResult<UserProfile>> PostUserProfile(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile); // Add new profile to the context
            await _context.SaveChangesAsync(); // Save changes to the database

            return CreatedAtAction("GetUserProfile", new { id = userProfile.Id }, userProfile); // Return the created profile
        }

        [HttpPut("{id}")] // PUT: api/userprofiles/{id}
        public async Task<IActionResult> PutUserProfile(int id, UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return BadRequest(); // Return 400 if IDs do not match
            }

            _context.Entry(userProfile).State = EntityState.Modified; // Mark profile as modified

            try
            {
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileExists(id))
                {
                    return NotFound(); // Return 404 if profile does not exist
                }
                else
                {
                    throw; // Rethrow the exception if it is a different issue
                }
            }

            return NoContent(); // Return 204 for successful update
        }

        [HttpDelete("{id}")] // DELETE: api/userprofiles/{id}
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id); // Find profile by ID
            if (userProfile == null)
            {
                return NotFound(); // Return 404 if not found
            }

            _context.UserProfiles.Remove(userProfile); // Remove the profile
            await _context.SaveChangesAsync(); // Save changes to the database

            return NoContent(); // Return 204 for successful deletion
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.Id == id); // Check if the profile exists
        }
    }
}