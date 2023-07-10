using Entity.Concrete;
using Newtonsoft.Json.Linq;

namespace Business.Utils
{
    public static class Messages
    {
        // User
        public const string EmailCannotBeNull = "Email Boş Bırakılamaz.";
        public const string PasswordCannotBeNull = "Şifre Boş Bırakılamaz.";
        public const string IdNotFound = "Id Bulunamadı.";
        public const string UserNotFound = "Kullanıcı Bulunamadı.";
        public const string IncorrectPassword = "Geçersiz Şifre.";
        public const string RegisterSuccess = "Kayıt Başarılı.";
        public const string RemoveSuccess = "Kullanıcı Başarıyla Silindi.";
        public const string UserAlreadyExists = "Girilen Email Başka Hesap Tarafından Kullanılıyor.";
        public const string NotAllowedToDelete = "Kullanıcı Silmeye Yetkiniz Yok.";

        // User Role
        public const string UserRoleAlreadyExists = "Kullanıcı Rolü Zaten Bulunuyor.";
        public const string UserRoleCreateSuccess = "Kullanıcı Rolü Başarıyla Oluşturuldu.";
        public const string UserRoleNotFound = "Kullanıcı Rolü Bulunamadı.";
        public const string UserRoleDeleteSuccess = "Kullanıcı Rolü Başarıyla Silindi.";

        // User Role Management
        public const string UserRoleUpdateSuccess = "Roller Başarıyla Değiştirildi.";
        public const string UserRoleNotModified = "Rol Değişikliği Bulunamadı.";
        
        // Role
        public const string RoleNotFound = "Rol Bulunamadı.";
        public const string RoleNameCannotBeNull = "Rol Adı Boş Olamaz.";
        public const string RoleAlreadyExist = "Role Zaten Bulunuyor.";
        public const string RoleCreateSuccess = "Rol Başarıyla Oluşturuldu.";
        public const string RoleUpdateSuccess = "Rol Başarıyla Güncellendi.";
        public const string RoleDeleteSuccess = "Rol Başarıyla Silindi.";

        // Category
        public const string CategoryCreateSuccess = "Kategori Başarıyla Oluşturuldu.";
        public const string CategoryDeleteSuccess = "Kategori Başarıyla Silindi.";
        public const string CategoryUpdateSuccess = "Kategori Başarıyla Güncellendi.";
        public const string CategoryAlreadyExist = "Rol Bulunamadı.";
        public const string CategoryNotFound = "Kategori Bulunamadı.";
        public const string CategoryNameCannotBeNull = "Kategori Adı Boş Olamaz.";

        // Article
        public const string ArticleCreateSuccess = "Haber Başarıyla Oluşturuldu.";
        public const string ArticleDeleteSuccess = "Haber Başarıyla Silindi.";
        public const string ArticleUpdateSuccess = "Haber Başarıyla Güncellendi.";
        public const string ArticleNotFound = "Haber Bulunamadı.";
        public const string AnArticleAbleToModifyByOwner = "Haberi Sadece Oluşturucusu Değiştirebilir.";
        public const string TitleCannotBeNull = "Başlık Boş Bırakılamaz.";
        public const string ContentCannotBeNull = "İçerik Boş Bırakılamaz.";
    }
}
