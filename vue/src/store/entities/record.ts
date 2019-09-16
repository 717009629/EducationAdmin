import Entity from './entity'
import Customer from './customer'


export default class Record extends Entity<number>{
    state?:string
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