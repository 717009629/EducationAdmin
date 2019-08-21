import Entity from './entity'
import User from './user'

export default class Student extends Entity<number>{
    name?: string
    province?: string
    city?: string
    district?:string
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
    teacherId?: number
    teacher?:User
    isDeleted?: boolean
    deleterUserId?: number
    deletionTime?: string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
    schoolBegin?: Date
    chinese?: number
    math?: number
    english?: number
    biology?: number
    history?: number
    geography?: number
    physics?: number
    chemistry?: number
    political?: number
    examination?:number
    privateLesson?: string
    classRank?: number
    gradeRank?: number

}