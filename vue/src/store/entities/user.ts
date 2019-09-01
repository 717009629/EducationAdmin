import Entity from './entity'
export default class User extends Entity<number>{
    password: string;
    sex?: boolean
    birthday?: Date

    identityCard?: string

    province?: string

    city?: string

    district?: string

    politicsStatus?: string

    schoolTag?: string

    educationBackground?: string

    nation?: string

    maritalStatus?: string

    workingYears?: number
}