import Entity from './entity'
import ContracState from './contractState'

export default class Contract extends Entity<Number>{
    startDate? :  string  
    endDate? :  string  
    fullMoney :  number  =0
    state? :  ContracState
    note? :  string  
    auditedReson? :  string  
    studentId? :  number  
    studentName? :  string  
    auditorId? :  number  
    auditorName? :  string  
    salesmanId? :  number  
    salesmanName? :  string  
    lastModificationTime? :  string  
    lastModifierUserId? :  number  
    creationTime? :  string  
    creatorUserId? :  number  
    orderId?:number
}