import Entity from './entity'

export default class Teacher extends Entity<number>{
    userName: string
    name: string
    emailAddress?: string
    birthday?: Date
    sex?: boolean
    phone?: string
    isActive?: boolean
    fullName?: string
    lastLoginTime?: string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number

}