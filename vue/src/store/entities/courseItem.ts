import Entity from './entity'

export default class CourseItem extends Entity<number>{
    orderId? :  number  
    courseId? :  number  
    courseName? :  string  
    count? :  number  
    fullMoney? :  number  
    note? :  string   
    lastModificationTime? :  string  
    lastModifierUserId? :  number  
    creationTime? :  string  
    creatorUserId? :  number  

}