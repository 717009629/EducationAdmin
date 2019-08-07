import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import Record from '../entities/record'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface RecordState extends ListState<Record>{
    editRecord:Record
}
class RecordMutations extends ListMutations<Record>{

}
class RecordModule extends ListModule<RecordState,any,Record>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Record>(),
        loading:false,
        editRecord:new Record()
    }
    actions={
        async getAll(context:ActionContext<RecordState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Record/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Record>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async getRecordsByStudentId(context:ActionContext<RecordState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Record/GetRecordsByStudentId',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Record>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<RecordState,any>,payload:any){
            await Ajax.post('/api/services/app/Record/Create',payload.data);
        },
        async update(context:ActionContext<RecordState,any>,payload:any){
            await Ajax.put('/api/services/app/Record/Update',payload.data);
        },
        async delete(context:ActionContext<RecordState,any>,payload:any){
            await Ajax.delete('/api/services/app/Record/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<RecordState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Record/Get?Id='+payload.id);
            return reponse.data.result as Record;
        }
    };
    mutations={
        setCurrentPage(state:RecordState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:RecordState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:RecordState,record:Record){
            state.editRecord=record;
        }
    }
}
const  recordModule=new RecordModule();
export default recordModule;