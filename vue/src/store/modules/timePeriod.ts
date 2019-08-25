import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Student from '../entities/student'
import TimePeriod from '../entities/timePeriod'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface TimePeriodState extends ListState<TimePeriod>{
    editTimePeriod:TimePeriod
}
class TimePeriodMutations extends ListMutations<TimePeriod>{

}
class TimePeriodModule extends ListModule<TimePeriodState,any,TimePeriod>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<TimePeriod>(),
        loading:false,
        editTimePeriod:new TimePeriod()
    }
    actions={
        async getAll(context:ActionContext<TimePeriodState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/TimePeriod/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<TimePeriod>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<TimePeriodState,any>,payload:any){
            await Ajax.post('/api/services/app/TimePeriod/Create',payload.data);
        },
        async update(context:ActionContext<TimePeriodState,any>,payload:any){
            await Ajax.put('/api/services/app/TimePeriod/Update',payload.data);
        },
        async delete(context:ActionContext<TimePeriodState,any>,payload:any){
            await Ajax.delete('/api/services/app/TimePeriod/Delete?Id='+payload.data.id);
        },
        async get(context:ActionContext<TimePeriodState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/TimePeriod/Get?Id='+payload.id);
            return reponse.data.result as TimePeriod;
        }
    };
    mutations={
        setCurrentPage(state:TimePeriodState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:TimePeriodState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:TimePeriodState,timePeriod:TimePeriod){
            state.editTimePeriod=timePeriod;
        }
    }
}
const  timePeriodModule=new TimePeriodModule();
export default timePeriodModule;