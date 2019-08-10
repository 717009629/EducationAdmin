import Entity from './entity'

export default class Course extends Entity<number>{
    category? :  string  
    name? :  string  
    price? :  number  =1
    lastModificationTime? :  string  
    lastModifierUserId? :  number  
    creationTime? :  string  
    creatorUserId? :  number  
    id:  number  
}