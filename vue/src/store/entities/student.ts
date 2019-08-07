import Entity from './entity'

export default class Student extends Entity<number>{
    name?: string
    province?: string
    city?: string
    address?: string
    sex?: boolean
    parent?: string
    relation?: string
    phone?: string
    fatherPhone?: string
    motherPhone?: string
    school?: string
    birthday?: string
    grade?: string
    courseType?: string
    studentType?: string
    teachMethod?: string
    origin?: string
    note?: string
    salesmanId?: number
    isDeleted?: boolean
    deleterUserId?: number
    deletionTime?: string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number

}