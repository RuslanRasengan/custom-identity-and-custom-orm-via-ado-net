using Auth.Models;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.IdentityProvider
{
    public class CustomRoleStore : IRoleStore<AppRole>
    {
        private readonly Context<AppRole> dbQueries;

        public CustomRoleStore(Context<AppRole> dbQueries)
        {
            this.dbQueries = dbQueries;
        }

        public async Task<IdentityResult> CreateAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));

            dbQueries.Create(role);

            var result = role;

            if (result != null)
            {
                return await Task.FromResult(IdentityResult.Success);
            }

            return IdentityResult.Failed(new IdentityError { Description = $"Could not create role{role}." });
        }

        public async Task<IdentityResult> DeleteAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));

            dbQueries.Delete(role);

            return await Task.FromResult(IdentityResult.Success);
        }



        public Task<AppRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (roleId == null) throw new ArgumentNullException(nameof(roleId));

            if (!Int32.TryParse(roleId, out int idInt))
            {
                throw new ArgumentException("Not a valid id", nameof(roleId));
            }

            var result = dbQueries.GetById(idInt);

            if (result == null)
            {
                throw new ArgumentException("This is not found", nameof(roleId));
            }

            return Task.FromResult(result);
        }

        public Task<AppRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedRoleName == null) throw new ArgumentNullException(nameof(normalizedRoleName));

            var roles = dbQueries.GetAll();

            var role = roles.FindAll(name => name.NormalizedRoleName == normalizedRoleName).FirstOrDefault();

            if (role == null)
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }

            return Task.FromResult(role);
        }

        public Task<string> GetNormalizedRoleNameAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));

            var result = dbQueries.GetById(role.Id).Name;

            if (String.IsNullOrEmpty(result))
            {
                throw new ArgumentException("This user name not found", nameof(role));
            }

            return Task.FromResult(result);
        }

        public async Task<string> GetRoleIdAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));

            var userId = dbQueries.GetById(role.Id).Id.ToString();

            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("This role not have password hash", nameof(role));
            }

            return await Task.FromResult(userId);
        }

        public async Task<string> GetRoleNameAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));

            var userName = dbQueries.GetById(role.Id).RoleName;

            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("This user not have role name", nameof(role));
            }

            return await Task.FromResult(userName);
        }

        public async Task SetNormalizedRoleNameAsync(AppRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            if (string.IsNullOrWhiteSpace(normalizedName)) throw new ArgumentNullException(nameof(normalizedName));

            role.NormalizedName = normalizedName;

            await Task.FromResult<object>(null);
        }

        public async Task SetRoleNameAsync(AppRole role, string roleName, CancellationToken cancellationToken)
        {
            await SetNormalizedRoleNameAsync(role, roleName, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(AppRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));

            var getRole = dbQueries.GetById(role.Id);

            if (getRole == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Could not update role {role}." });
            }

            return await Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
        }
    }
}
