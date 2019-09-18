import Entity from './entity'
export default class ChangePasswordByAdmin extends Entity<number>{
    password: string;
    confirmPassword:string;
}