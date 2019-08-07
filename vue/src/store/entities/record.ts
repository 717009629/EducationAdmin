import Entity from './entity'

export default class Record extends Entity<number>{
    category?: string
    progress?: string
    date?: string
    content?: string
    studentId?: number
    studentName?: string
    salesmanId?: number
    salesmanName?: string
    tenantId?: number
    isDeleted?: boolean
    deleterUserId?: number
    deletionTime?: string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}