import Entity from './entity'
import CustomerState from './customerState';

export default class Customer extends Entity<number>{
    studentName?: string
    mother?: string
    father?: string
    fatherPhone?: string
    motherPhone?: string
    otherGuardian?: string
    otherGuadianPhone?: string
    note?: string
    state:CustomerState
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}