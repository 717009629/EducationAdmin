import Entity from './entity'


export default class Option extends Entity<number>{
    isAcitve:boolean
    name?:string
    category?:string
    order?:number
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}