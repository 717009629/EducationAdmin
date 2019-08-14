import Entity from './entity'
import Customer from './customer'
import CustomerState from './customerState';


export default class Record extends Entity<number>{
    state?:CustomerState
    date?: string
    content?: string
    customerId: number
    salesmanId: number
    salesmanName?: string
    tenantId?: number
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
    student?:Customer
}