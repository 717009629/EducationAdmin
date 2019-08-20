import Entity from './entity'
export default class User extends Entity<number>{
    password:string;
    sex?:boolean
    birthday:Date
}