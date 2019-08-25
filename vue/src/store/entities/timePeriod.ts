import Entity from './entity'


export default class TimePeriod extends Entity<number>{
    start: string
    end: string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}