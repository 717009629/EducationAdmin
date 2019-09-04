import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import LessonAttendance from '../entities/lessonAttendance'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface LessonAttendanceState extends ListState<LessonAttendance>{
    editLessonAttendance:LessonAttendance
}
class LessonAttendanceMutations extends ListMutations<LessonAttendance>{

}
class LessonAttendanceModule extends ListModule<LessonAttendanceState,any,LessonAttendance>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<LessonAttendance>(),
        loading:false,
        editLessonAttendance:new LessonAttendance()
    }
    actions={
        async getAll(context:ActionContext<LessonAttendanceState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/LessonAttendance/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<LessonAttendance>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<LessonAttendanceState,any>,payload:any){
            await Ajax.post('/api/services/app/LessonAttendance/Create',payload.data);
        },
        async update(context:ActionContext<LessonAttendanceState,any>,payload:any){
            await Ajax.put('/api/services/app/LessonAttendance/Update',payload.data);
        },
        async delete(context:ActionContext<LessonAttendanceState,any>,payload:any){
            await Ajax.delete('/api/services/app/LessonAttendance/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<LessonAttendanceState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/LessonAttendance/Get?Id='+payload.id);
            return reponse.data.result as LessonAttendance;
        }
    };
    mutations={
        setCurrentPage(state:LessonAttendanceState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:LessonAttendanceState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:LessonAttendanceState,lessonAttendance:LessonAttendance){
            state.editLessonAttendance=lessonAttendance;
        }
    }
}
const  lessonAttendanceModule=new LessonAttendanceModule();
export default lessonAttendanceModule;