import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Lesson from '../entities/lesson'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface LessonState extends ListState<Lesson>{
    editLesson:Lesson
}
class LessonMutations extends ListMutations<Lesson>{

}
class LessonModule extends ListModule<LessonState,any,Lesson>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Lesson>(),
        loading:false,
        editLesson:new Lesson()
    }
    actions={
        async getAll(context:ActionContext<LessonState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Lesson/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Lesson>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async getByStudentId(context:ActionContext<LessonState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Lesson/GetByStudentId',{params:payload.data});
            context.state.loading=false;
            context.state.list=reponse.data.result;
        },
        async create(context:ActionContext<LessonState,any>,payload:any){
            await Ajax.post('/api/services/app/Lesson/Create',payload.data);
        },
        async update(context:ActionContext<LessonState,any>,payload:any){
            await Ajax.put('/api/services/app/Lesson/Update',payload.data);
        },
        async delete(context:ActionContext<LessonState,any>,payload:any){
            await Ajax.delete('/api/services/app/Lesson/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<LessonState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Lesson/Get?Id='+payload.id);
            return reponse.data.result as Lesson;
        }
    };
    mutations={
        setCurrentPage(state:LessonState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:LessonState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:LessonState,lesson:Lesson){
            state.editLesson=lesson;
        }
    }
}
const  lessonModule=new LessonModule();
export default lessonModule;