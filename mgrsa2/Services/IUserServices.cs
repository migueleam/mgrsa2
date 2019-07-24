using Microsoft.AspNetCore.Mvc.Rendering;
using mgrsa2.Common;
using System.Collections.Generic;
using mgrsa2.Models;
using mgrsa2.Components;

namespace mgrsa2.Services
{
    public interface IUserServices
    {

        AppUser FindUserByUserName(string username);
        List<UserProfile> GetUserProfiles();
        UserProfile GetUserProfiles(string Id, int loginId, int profileId);
        List<Profile> GetProfiles();
        List<Profile> GetLogins();
        List<Profile> GetLoginsAdo();
        List<PhoneProvider> GetPhoneProviders();


        List<DirectoryItem> GetDirectory();
        List<DirectoryItem> GetDirectory(string search);
        List<Profile> GetDirectoryProfile(string search);


        List<SelectListItem> GetSupervisores();
        List<SelectListItem> GetCodigos();
        List<SelectListItem> GetProveedores();
        List<SelectListItem> GetContactosPermitidos();
        List<SelectListItem> GetLevels();


        ResponseModel AddLogin(UserFormModel login);
        ResponseModel AddProfile(UserFormModel login);

        Profile GetProfile(int Id);
        ProfileForm GetProfileForm(string userId, int loginId, int profileId);

        Profile GetProfileByLoginId(int loginId);


        List<EntryCard> GetEntryCards(string userId, int loginId, int profileId, string card);
        ResponseModel AddEntryCard(EntryCard card);
        ResponseModel UpdateEntryCard(EntryCard card);

        List<RoleForm> GetRolesForm(string userId);
        ResponseModel AddRole(UserRole userrole);
        ResponseModel RemoveRole(UserRole userrole);
        List<string> GetRoles(string userId);

        ResponseModel UpdateProfile(ProfileForm profile);
        
        ResponseModel SetProfile(ProfileForm profile);

        ProfileForm GetProfileForm(int Id);
        UserFormModel GetUserForm(string Id);

        Profile GetCurrentUser();
        Profile GetCurrentUser(string userId);

        Profile GetProfile(string userId);
        ResponseModel PassLogProf();

        ResponseModel UpdateMyProfile(myProfile profile);

    }
}
