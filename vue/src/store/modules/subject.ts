import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Subject from '../entities/subject'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface SubjectState extends ListState<Subject>{
    editSubject:Subject
}
class SubjectMutations extends ListMutations<Subject>{

}
class SubjectModule extends ListModule<SubjectState,any,Subject>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Subject>(),
        loading:false,
        editSubject:new Subject()
    }
    actions={
        async getAll(context:ActionContext<SubjectState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Subject/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Subject>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<SubjectState,any>,payload:any){
            await Ajax.post('/api/services/app/Subject/Create',payload.data);
        },
        async update(context:ActionContext<SubjectState,any>,payload:any){
            await Ajax.put('/api/services/app/Subject/Update',payload.data);
        },
        async delete(context:ActionContext<SubjectState,any>,payload:any){
            await Ajax.delete('/api/services/app/Subject/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<SubjectState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Subject/Get?Id='+payload.id);
            return reponse.data.result as Subject;
        }
    };
    mutations={
        setCurrentPage(state:SubjectState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:SubjectState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:SubjectState,subject:Subject){
            state.editSubject=subject;
        }
    }
}
const  subjectModule=new SubjectModule();
export default subjectModule;