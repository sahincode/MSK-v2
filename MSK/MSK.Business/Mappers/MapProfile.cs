using AutoMapper;
using MSK.Business.DTOs.AccredationModelDTOs;
using MSK.Business.DTOs.CalendarPlanModelDTOs;
using MSK.Business.DTOs.CandidateModelDTOs;
using MSK.Business.DTOs.ContactModelDTOs;
using MSK.Business.DTOs.DecisionModelDTOs;
using MSK.Business.DTOs.ElectionModelDTOs;
using MSK.Business.DTOs.HistoryModelDTOs;
using MSK.Business.DTOs.HomeSlideDTOs;
using MSK.Business.DTOs.InfoModelDTOs;
using MSK.Business.DTOs.InstructionModelDTOs;
using MSK.Business.DTOs.LegislationModelDTOs;
using MSK.Business.DTOs.NationalAttributeModelDTOs;
using MSK.Business.DTOs.PressNewDTOs;
using MSK.Business.DTOs.ReferendumModelDTOs;
using MSK.Business.DTOs.SettingModelDTOs;
using MSK.Business.DTOs.SubDecisionModelDTOs;
using MSK.Business.DTOs.SubInstructionModelDTOs;
using MSK.Business.DTOs.VoterModelDTOs;
using MSK.Core.Models;

namespace MSK.Business.Mappers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            //setting model mapping profile
            CreateMap<SettingUpdateDto, Setting>().ReverseMap();
            CreateMap<SettingGetDto, Setting>().ReverseMap();
            //Home slide model mapping profile
            CreateMap<HomeSlideCreateDto, HomeSlide>().ReverseMap();
            CreateMap<HomeSlideUpdateDto, HomeSlide>().ReverseMap();
            CreateMap<HomeSlideIndexDto, HomeSlide>().ReverseMap();
            CreateMap<HomeSlideLayoutDto, HomeSlide>().ReverseMap();
            //press new  model model mapping profile
            CreateMap<PressNewCreateDto, PressNew>().ReverseMap();
            CreateMap<PressNewUpdateDto, PressNew>().ReverseMap();
            CreateMap<PressNewIndexDto, PressNew>().ReverseMap();
            CreateMap<PressNewLayoutDto, PressNew>().ReverseMap();
            //history  model model mapping profile
            CreateMap<HistoryCreateDto, History>().ReverseMap();
            CreateMap<HistoryUpdateDto, History>().ReverseMap();
            CreateMap<HistoryIndexDto, History>().ReverseMap();
            CreateMap<HistoryLayoutDto, History>().ReverseMap();
            //national attribute model model mapping profile
            CreateMap<NationalAttributeCreateDto, NationalAttribute>().ReverseMap();
            CreateMap<NationalAttributeUpdateDto, NationalAttribute>().ReverseMap();
            CreateMap<NationalAttributeIndexDto, NationalAttribute>().ReverseMap();
            CreateMap<NationalAttributeLayoutDto, NationalAttribute>().ReverseMap();
            // accredation model model mapping profile
            CreateMap<AccredationCreateDto, Accredation>().ReverseMap();
            CreateMap<AccredationUpdateDto, Accredation>().ReverseMap();
            CreateMap<AccredationIndexDto, Accredation>().ReverseMap();
            CreateMap<AccredationLayoutDto, Accredation>().ReverseMap();
            // legislation model model mapping profile
            CreateMap<LegislationCreateDto, Legislation>().ReverseMap();
            CreateMap<LegislationUpdateDto, Legislation>().ReverseMap();
            CreateMap<LegislationIndexDto, Legislation>().ReverseMap();
            CreateMap<LegislationLayoutDto, Legislation>().ReverseMap();
            // decision model model mapping profile
            CreateMap<DecisionCreateDto, Decision>().ReverseMap();
            CreateMap<DecisionUpdateDto, Decision>().ReverseMap();
            CreateMap<DecisionIndexDto, Decision>().ReverseMap();
            CreateMap<DecisionLayoutDto, Decision>().ReverseMap();
            // sub  decision model model mapping profile
            CreateMap<SubDecisionCreateDto, SubDecision>().ReverseMap();
            CreateMap<SubDecisionUpdateDto, SubDecision>().ReverseMap();
            CreateMap<SubDecisionIndexDto, SubDecision>().ReverseMap();
            CreateMap<SubDecisionLayoutDto, SubDecision>().ReverseMap();
            //instruction model model mapping profile
            CreateMap<InstructionCreateDto, Instruction>().ReverseMap();
            CreateMap<InstructionUpdateDto, Instruction>().ReverseMap();
            CreateMap<InstructionIndexDto, Instruction>().ReverseMap();
            CreateMap<InstructionLayoutDto, Instruction>().ReverseMap();
            // sub instruction model model mapping profile
            CreateMap<SubInstructionCreateDto, SubInstruction>().ReverseMap();
            CreateMap<SubInstructionUpdateDto, SubInstruction>().ReverseMap();
            CreateMap<SubInstructionIndexDto, SubInstruction>().ReverseMap();
            CreateMap<SubInstructionLayoutDto, SubInstruction>().ReverseMap();
            // contact model model mapping profile
            CreateMap<ContactCreateDto, Contact>().ReverseMap();
            CreateMap<ContactUpdateDto, Contact>().ReverseMap();
            CreateMap<ContactIndexDto, Contact>().ReverseMap();
            CreateMap<ContactLayoutDto, Contact>().ReverseMap();
            //  Info  model model mapping profile
            CreateMap<InfoCreateDto, Info>().ReverseMap();
            CreateMap<InfoUpdateDto, Info>().ReverseMap();
            CreateMap<InfoIndexDto,Info>().ReverseMap();
            CreateMap<InfoLayoutDto, Info>().ReverseMap();
            // CalendarPlan  model model mapping profile
            CreateMap<CalendarPlanCreateDto, CalendarPlan>().ReverseMap();
            CreateMap<CalendarPlanUpdateDto, CalendarPlan>().ReverseMap();
            CreateMap<CalendarPlanIndexDto, CalendarPlan>().ReverseMap();
            CreateMap<CalendarPlanLayoutDto, CalendarPlan>().ReverseMap();
            // referendum model model mapping profile
            CreateMap<ReferendumCreateDto, Referendum>().ReverseMap();
            CreateMap<ReferendumUpdateDto, Referendum>().ReverseMap();
            CreateMap<ReferendumIndexDto, Referendum>().ReverseMap();
            CreateMap<ReferendumLayoutDto, Referendum>().ReverseMap();
            // voter model model mapping profile
            CreateMap<VoterCreateDto, Voter>().ReverseMap();
            CreateMap<VoterUpdateDto, Voter>().ReverseMap();
            CreateMap<VoterIndexDto, Voter>().ReverseMap();
            CreateMap<VoterLayoutDto, Voter>().ReverseMap();
            // candidate model model mapping profile
            CreateMap<CandidateCreateDto, Candidate>().ReverseMap();
            CreateMap<CandidateUpdateDto, Candidate>().ReverseMap();
            CreateMap<CandidateIndexDto, Candidate>().ReverseMap();
            CreateMap<CandidateLayoutDto, Candidate>().ReverseMap();
            // election model model mapping profile
            CreateMap<ElectionCreateDto, Election>().ReverseMap();
            CreateMap<ElectionIndexDto, Election>().ReverseMap();
            CreateMap<ElectionLayoutDto, Election>().ReverseMap();
            CreateMap<ElectionUpdateDto, Election>().ReverseMap();
        }
    }
}
