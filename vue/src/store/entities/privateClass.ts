import Entity from './entity'
import Student from './student'
import Course from './course'
import Contract from './contract'
import Class from './class';

export default class PrivateClass extends Entity<number>{
    orderDate? :  string  
    schoolBegin? :  string  
    fullMoney? :  number  
    count? :  number  
    state? :  number  
    class? :  Class  
    studentId :  number  
    student? :  Student  
    salesmanId :  number  
    lastModificationTime? :  string  
    lastModifierUserId? :  number  
    creationTime? :  string  
    creatorUserId? :  number  
    note? : string
    contract?:Contract

}