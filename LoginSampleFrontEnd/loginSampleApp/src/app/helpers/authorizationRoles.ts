export const AdminRole:string = "Admin";
export const WriterRole:string = "Writer";
export const ReaderRole:string = "Reader";

export class AuthorizationRoles{
    static get Admin(){
        return AdminRole;
    }

    static get Writer(){
        return WriterRole;
    }

    static get Reader(){
        return ReaderRole;
    }

}