import Entity from './entity'
import Student from './student'
import Lesson from './lesson'
import Contract from './contract'
import Order from './order';

export default class LessonAttendance extends Entity<number>{

    lessonId: number
    lesson?: Lesson
    orderId: number
    order: Order
    attended: boolean
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
    note?: string
    contract?: Contract

}