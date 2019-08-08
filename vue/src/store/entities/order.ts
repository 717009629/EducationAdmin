import Entity from './entity'

export default class Order extends Entity<number>{
    orderDate? :  string  
    schoolBegin? :  string  
    fullMoney? :  number  
    state? :  string  
    className? :  string  
    studentId? :  number  
    studentName? :  string  
    salesmanId? :  number  
    salesmanName? :  string  
    lastModificationTime? :  string  
    lastModifierUserId? :  number  
    creationTime? :  string  
    creatorUserId? :  number  
    note? : string
}