import Entity from './entity'


export default class Subject extends Entity<number>{
    isAcitve:boolean
    name?:string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}