import Entity from './entity'
import User from './user';
import Student from './student';
import Order from './order';
import Class from './class';
import TimePeriod from './timePeriod';

export default class Lesson extends Entity<number>{
    lessonDate: Date
    classId: number
    clas?: Class
    orderId: number
    order?: Order
    timePeriodId:number
    start:string
    end:string
    startTime:Date
    endTime:Date
    isFinish: boolean
    teacherId: number
    teacher?: User
    course?: string
    lastModificationTime?: string
    lastModifierUserId?: number
    creationTime?: string
    creatorUserId?: number
}