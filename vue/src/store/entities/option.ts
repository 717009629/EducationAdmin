import Entity from './entity'


export default class TimePeriod extends Entity<number>{
    isAcitve:boolean
    name?:string
    category?:string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}