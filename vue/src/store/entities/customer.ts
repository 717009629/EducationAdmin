import Entity from './entity'

export default class Customer extends Entity<number>{
    studentName?: string
    mother?: string
    father?: string
    fatherPhone?: string
    motherPhone?: string
    otherGuardian?: string
    otherGuadianPhone?: string
    note?: string
    state:string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}