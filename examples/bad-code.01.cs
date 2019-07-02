using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using OBC.iPortal.UI;
using Entities = OBC.iPortal.Business.Entities;
using OBC.iPortal.Business.Facade;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Configuration;


namespace OBC.iPortal.UI
{
    public partial class Export : PageBase
    {

        #region Constantes

        #region Public
        public const string QUERYSTRING_FORMATO = "format";
        public const string FORMATO_MAIL = "mail";
        public const string SESSION_CLIPPING = "Export.aspx.cs_Clipping";
        public const string SESSION_DETAIL = "Export.aspx.cs_ClippingDetail";
        public const string SESSION_ANALISE = "Export.aspx.cs_ClippingAnalise";
        public const string SESSION_SELECIONADOS = "Export.aspx.cs_ClippingsSelected";
        #endregion

        #region Private
        protected const string FORMATO_CSV = "csv";
        protected const string FORMATO_PDF = "pdf";
        protected const string FORMATO_XLSX = "xlsx";
        protected const string FORMATO_DOCX = "docx";

        protected const string SESSION_OFFLINE = "Export.aspx.cs_ServiceOffline";
        protected const string SESSION_GRUPOS = "Export.aspx.cs_ContactGroups";
        protected const string SESSION_CONTATOS = "Export.aspx.cs_Contacts";
        protected const string SESSION_EMAILS = "Export.aspx.cs_ContactEmails";

        protected const int DESTINATARIO_PARA = 0;
        protected const int DESTINATARIO_CC = 1;
        protected const int DESTINATARIO_CCO = 2;
        #endregion

        #endregion

        #region Properties

        #region Private
        protected List<Entities.ExportItem> m_Fields = null;

        protected List<Entities.ExportItem> Fields
        {
            get
            {
                if (m_Fields == null)
                {
                    m_Fields = new List<Entities.ExportItem>();
                }

                return m_Fields;
            }
            set
            {
                m_Fields = value;
            }
        }

        protected IEnumerable<string> ListaDeDestinatarios
        {
            get
            {
                string destinatarios = txtDestinatarios.Text;

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[a-z0-9\._%-]+@[a-z0-9\.-]+\.[a-z]{2,4}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                System.Text.RegularExpressions.MatchCollection collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    yield return match.Value;
                }
                regex = new System.Text.RegularExpressions.Regex(@"[\[][^\]\[]*[\(]([0-9]+)[\)][^\]\[]*[\]]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    int idGrupoContato = 0;
                    if (match.Groups != null && match.Groups.Count > 1)
                    {
                        if (Int32.TryParse(match.Groups[1].Value, out idGrupoContato))
                        {
                            List<Entities.Contato> contatosDoGrupo = TodosOsContatos.FindAll(contato => contato.IDGrupoContato == idGrupoContato);

                            if (contatosDoGrupo != null)
                            {
                                foreach (Entities.Contato contato in contatosDoGrupo)
                                {
                                    yield return contato.Email;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected int QtdItemsListaDeDestinatarios
        {
            get
            {
                List<string> item = new List<string>();
                string destinatarios = txtDestinatarios.Text;

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[a-z0-9\._%-]+@[a-z0-9\.-]+\.[a-z]{2,4}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                System.Text.RegularExpressions.MatchCollection collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    item.Add(match.Value);
                }
                regex = new System.Text.RegularExpressions.Regex(@"[\[][^\]\[]*[\(]([0-9]+)[\)][^\]\[]*[\]]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    if (match.Groups != null && match.Groups.Count > 1)
                    {
                        item.Add(match.Groups[0].Value);
                    }
                }
                return item.Count();
            }
        }

        protected IEnumerable<string> ListaDeCC
        {
            get
            {
                string destinatarios = txtCC.Text;

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[a-z0-9\._%-]+@[a-z0-9\.-]+\.[a-z]{2,4}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                System.Text.RegularExpressions.MatchCollection collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    yield return match.Value;
                }
                regex = new System.Text.RegularExpressions.Regex(@"[\[][^\]\[]*[\(]([0-9]+)[\)][^\]\[]*[\]]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    int idGrupoContato = 0;
                    if (match.Groups != null && match.Groups.Count > 1)
                    {
                        if (Int32.TryParse(match.Groups[1].Value, out idGrupoContato))
                        {
                            List<Entities.Contato> contatosDoGrupo = TodosOsContatos.FindAll(contato => contato.IDGrupoContato == idGrupoContato);

                            if (contatosDoGrupo != null)
                            {
                                foreach (Entities.Contato contato in contatosDoGrupo)
                                {
                                    yield return contato.Email;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected int QtdItemsListaDeCC
        {
            get
            {
                List<string> item = new List<string>();

                string destinatarios = txtCC.Text;

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[a-z0-9\._%-]+@[a-z0-9\.-]+\.[a-z]{2,4}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                System.Text.RegularExpressions.MatchCollection collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    item.Add(match.Value);
                }
                regex = new System.Text.RegularExpressions.Regex(@"[\[][^\]\[]*[\(]([0-9]+)[\)][^\]\[]*[\]]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    if (match.Groups != null && match.Groups.Count > 1)
                    {
                        item.Add(match.Groups[0].Value);
                    }
                }
                return item.Count();
            }
        }

        protected IEnumerable<string> ListaDeCCO
        {
            get
            {
                string destinatarios = txtCCO.Text;

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[a-z0-9\._%-]+@[a-z0-9\.-]+\.[a-z]{2,4}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                System.Text.RegularExpressions.MatchCollection collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    yield return match.Value;
                }
                regex = new System.Text.RegularExpressions.Regex(@"[\[][^\]\[]*[\(]([0-9]+)[\)][^\]\[]*[\]]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    int idGrupoContato = 0;
                    if (match.Groups != null && match.Groups.Count > 1)
                    {
                        if (Int32.TryParse(match.Groups[1].Value, out idGrupoContato))
                        {
                            List<Entities.Contato> contatosDoGrupo = TodosOsContatos.FindAll(contato => contato.IDGrupoContato == idGrupoContato);

                            if (contatosDoGrupo != null)
                            {
                                foreach (Entities.Contato contato in contatosDoGrupo)
                                {
                                    yield return contato.Email;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected int QtdItemsListaDeCCO
        {
            get
            {
                List<string> items = new List<string>();

                string destinatarios = txtCCO.Text;

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"[a-z0-9\._%-]+@[a-z0-9\.-]+\.[a-z]{2,4}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                System.Text.RegularExpressions.MatchCollection collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    items.Add(match.Value);
                }
                regex = new System.Text.RegularExpressions.Regex(@"[\[][^\]\[]*[\(]([0-9]+)[\)][^\]\[]*[\]]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                collection = regex.Matches(destinatarios);

                foreach (System.Text.RegularExpressions.Match match in collection)
                {
                    if (match.Groups != null && match.Groups.Count > 1)
                    {
                        items.Add(match.Groups[0].Value);
                    }
                }
                return items.Count();
            }
        }

        protected List<Entities.GrupoContato> GruposDeContatos
        {
            get
            {

                if (Session[SESSION_GRUPOS] == null)
                {
                    Session[SESSION_GRUPOS] = uiAdmin.GetGrupoContatoByParameters(appSession.UsuarioAtivo.IdUsuario, appSession.EmpresaAtiva.IdEmpresa, null, String.Empty, String.Empty);
                }

                return (List<Entities.GrupoContato>)Session[SESSION_GRUPOS];
            }
        }

        protected List<Entities.Contato> TodosOsContatos
        {
            get
            {
                if (Session[SESSION_CONTATOS] == null)
                {
                    List<Entities.Contato> listaComTodosOsContatos = new List<Entities.Contato>();

                    if (GruposDeContatos != null && GruposDeContatos.Count > 0)
                    {
                        foreach (Entities.GrupoContato grupo in GruposDeContatos)
                        {
                            List<Entities.Contato> contatosDoGrupo = uiAdmin.GetContatosByGrupo(grupo.IdGrupoContato);

                            if (contatosDoGrupo != null && contatosDoGrupo.Count > 0)
                            {
                                listaComTodosOsContatos.AddRange(contatosDoGrupo);
                            }
                        }
                    }

                    Session[SESSION_CONTATOS] = listaComTodosOsContatos;
                }

                return (List<Entities.Contato>)Session[SESSION_CONTATOS];
            }
        }

        protected List<Entities.Clipping> Clippings
        {
            get
            {
                if (Session[SESSION_CLIPPING] == null)
                {
                    List<Entities.Clipping> listaTemporaria = uiAdmin.ExportClippingByXML(appSession.ItensSelecionadosXML);

                    if (listaTemporaria.Count != appSession.ClippingsSelecao.IdClipping_ToExport.Count)
                    {
                        bool estaServicoOffline = false;

                        List<int> buffer = new List<int>();

                        appSession.ClippingsSelecao.IdClipping_ToExport.ForEach(item => buffer.Add(item));

                        listaTemporaria.ForEach(item => buffer.Remove(item.IDClipping));

                        if (Formato == FORMATO_MAIL)
                        {

                            if (buffer.Count > 0)
                            {

                                ((iPortal_User)Page.Master).ShowMessage("Alerta", "Entre os clippings selecionados h� itens offline.<br/>N�o � poss�vel enviar emails de clippings offline.<br/><br/>Os clippings offline desta sele��o ser�o desconsiderados no envio deste email.");

                                foreach (int b in buffer)
                                    appSession.ClippingsSelecao.IdClipping_ToExport.Remove(b);

                            }

                        }
                        else
                        {
                            listaTemporaria.AddRange(uiAdmin.BuscaClippingsOffline(buffer, appSession.EmpresaAtiva.IdEmpresa, out estaServicoOffline));
                        }

                        IsServiceOffline = estaServicoOffline;
                    }

                    Session[SESSION_CLIPPING] = listaTemporaria;
                }

                if (Formato != FORMATO_MAIL)
                    return (from c in ((List<Entities.Clipping>)Session[SESSION_CLIPPING]) orderby c.Upload descending, c.idUpload descending, c.Ordem ascending select c).ToList();
                else
                    return ((List<Entities.Clipping>)Session[SESSION_CLIPPING]);
            }
            set
            {
                Session[SESSION_CLIPPING] = value;
            }
        }

        protected List<Entities.ClippingDetail> Details
        {
            get
            {
                if (Session[SESSION_DETAIL] == null)
                {
                    List<Entities.ClippingDetail> listaTemporaria = uiAdmin.ExportClippingDetailByXML(appSession.ItensSelecionadosXML);

                    if (listaTemporaria.Count != appSession.ClippingsSelecao.IdClipping_ToExport.Count)
                    {
                        bool estaServicoOffline = false;

                        List<int> buffer = new List<int>();

                        appSession.ClippingsSelecao.IdClipping_ToExport.ForEach(item => buffer.Add(item));

                        listaTemporaria.ForEach(item => buffer.Remove(item.IDClipping));

                        listaTemporaria.AddRange(uiAdmin.BuscaClippingsDetailOffline(buffer, appSession.EmpresaAtiva.IdEmpresa, out estaServicoOffline));

                        IsServiceOffline = estaServicoOffline;
                    }

                    Session[SESSION_DETAIL] = listaTemporaria;
                }

                if (Formato != FORMATO_MAIL)
                    return (from d in ((List<Entities.ClippingDetail>)Session[SESSION_DETAIL]) orderby d.Upload descending, d.idUpload descending, d.Ordem ascending select d).ToList();
                else
                    return ((List<Entities.ClippingDetail>)Session[SESSION_DETAIL]);
            }
            set
            {
                Session[SESSION_DETAIL] = value;
            }
        }

        protected List<Entities.ClippingAnalise> Analysis
        {
            get
            {
                if (Session[SESSION_ANALISE] == null || (!(((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).Count > 0)))
                {
                    if (ContainsID("ClippingAnalise"))
                    {
                        bool servicoEstaOffline = false;
                        Session[SESSION_ANALISE] = uiAdmin.ExportClippinAnalisegByList(appSession.ClippingsSelecao.IdClipping_ToExport, out servicoEstaOffline);

                        if (servicoEstaOffline)
                        {
                            Session[SESSION_ANALISE] = new List<Entities.ClippingAnalise>();
                            IsServiceOffline = true;
                        }
                        else
                        {
                            if (!appSession.EmpresaAtiva.FoiEnriquecido)
                                appSession.EmpresaAtiva = uiAdmin.GetEmpresaCamposPersonalizados(appSession.EmpresaAtiva);

                            if (appSession.EmpresaAtiva.ServicoEstaOffline == false)
                            {
                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.temCampo1 = appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado1);
                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.CaptionCampo1 = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado1);

                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.temCampo2 = appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado2);
                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.CaptionCampo2 = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado2);

                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.temCampo3 = appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado3);
                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.CaptionCampo3 = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado3);

                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.temCampo4 = appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado4);
                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.CaptionCampo4 = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado4);

                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.temCampo5 = appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado5);
                                ((List<Entities.ClippingAnalise>)Session[SESSION_ANALISE]).ForEach(item => item.CaptionCampo5 = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado5);
                            }

                            IsServiceOffline = false;
                        }
                    }
                    else
                    {
                        Session[SESSION_ANALISE] = new List<Entities.ClippingAnalise>();
                    }
                }

                return (List<Entities.ClippingAnalise>)Session[SESSION_ANALISE];
            }
            set
            {
                Session[SESSION_ANALISE] = value;
            }
        }

        protected bool IsServiceOffline
        {
            get
            {
                if (Session[SESSION_OFFLINE] == null)
                {
                    Session[SESSION_OFFLINE] = false;
                }
                return (bool)Session[SESSION_OFFLINE];
            }
            set
            {
                Session[SESSION_OFFLINE] = value;
            }
        }

        protected List<Entities.ExportItem> ListaCampos
        {
            get
            {
                List<Entities.ExportItem> lista = new List<Entities.ExportItem>();

                switch (this.Selecao)
                {
                    case FORMATO_CSV:
                        lista = uiAdmin.ListaCamposCSV();
                        break;
                    case FORMATO_PDF:
                        lista = uiAdmin.ListaCamposPDF();
                        break;
                    case FORMATO_XLSX:
                        lista = uiAdmin.ListaCamposXLS();
                        break;
                    case FORMATO_DOCX:
                        lista = uiAdmin.ListaCamposDOC();
                        break;
                    case FORMATO_MAIL:
                        lista = uiAdmin.ListaCamposMAIL();
                        break;
                }

                if (!appSession.EmpresaAtiva.FoiEnriquecido)
                    appSession.EmpresaAtiva = uiAdmin.GetEmpresaCamposPersonalizados(appSession.EmpresaAtiva);

                List<Entities.ExportItem> itensParaExcluir = new List<Entities.ExportItem>();

                if (appSession.EmpresaAtiva.ServicoEstaOffline == false)
                {
                    int indice = 1;
                    foreach (Entities.ExportItem item in lista)
                    {
                        if (item.Trocar)
                        {
                            switch (indice)
                            {
                                case 1:
                                    item.Desc = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado1;
                                    indice++;
                                    if (!appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado1) itensParaExcluir.Add(item);
                                    break;
                                case 2:
                                    item.Desc = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado2;
                                    indice++;
                                    if (!appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado2) itensParaExcluir.Add(item);
                                    break;
                                case 3:
                                    item.Desc = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado3;
                                    indice++;
                                    if (!appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado3) itensParaExcluir.Add(item);
                                    break;
                                case 4:
                                    item.Desc = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado4;
                                    indice++;
                                    if (!appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado4) itensParaExcluir.Add(item);
                                    break;
                                case 5:
                                    item.Desc = appSession.EmpresaAtiva.AnaliseLabelCampoPersonalizado5;
                                    indice++;
                                    if (!appSession.EmpresaAtiva.AnaliseTemCampoPersonalizado5) itensParaExcluir.Add(item);
                                    break;
                            }
                        }
                    }
                }

                foreach (Entities.ExportItem item in itensParaExcluir)
                    lista.Remove(item);

                return lista;
            }
        }

        protected string Mensagem
        {
            get
            {
                switch (this.Selecao)
                {
                    case FORMATO_CSV:
                        return "Disponibilizar clippings em formato CSV.";
                    case FORMATO_PDF:
                        return "Disponibilizar clippings em formato PDF.";
                    case FORMATO_XLSX:
                        return "Disponibilizar clippings em formato Excel (.xlsx).";
                    case FORMATO_DOCX:
                        return "Disponibilizar clippings em formato Word (.docx).";
                    case FORMATO_MAIL:
                        return "Enviar clippings por email.";
                    default:
                        return "";
                }
            }
        }

        protected TextBox TextBoxDestinatario
        {
            get
            {
                switch (DestinatarioSelecionado)
                {
                    case DESTINATARIO_CC:
                        return txtCC;
                    case DESTINATARIO_CCO:
                        return txtCCO;
                    default:
                        return txtDestinatarios;
                }
            }
        }

        protected string URL
        {
            get
            {
                string protocolo = String.Empty;

                if (Request.IsSecureConnection)
                    protocolo = "https";
                else
                    protocolo = "http";

#if DEBUG
                return String.Format("{0}://{1}:{2}/", protocolo, Request.UrlReferrer.Host, Request.UrlReferrer.Port);
#else
                return String.Format("{0}://{1}", protocolo, Request.UrlReferrer.Host);
#endif
            }
        }

        #endregion

        #region Public
        public List<ItemSelecionado> DadosSelecionados
        {
            get
            {

                if (Session[SESSION_SELECIONADOS] == null)
                {

                    List<ItemSelecionado> listaDeItens = new List<ItemSelecionado>();

                    ItemSelecionado item = null;// new ItemSelecionado();

                    List<string> listaDeCategorias = new List<string>();

                    string categoria = String.Empty;


                    listaDeItens = new List<ItemSelecionado>();

                    foreach (Entities.Clipping itemDeClipping in Clippings)
                    {
                        listaDeCategorias = new List<string>();

                        if (itemDeClipping.Categoria == null || itemDeClipping.Categoria.Count == 0)
                            itemDeClipping.Categoria.Add("Sem categoria");

                        itemDeClipping.Categoria.ForEach(i => listaDeCategorias.Add(i));

                        foreach (string categoriaDoItem in listaDeCategorias)
                        {
                            item = new ItemSelecionado(itemDeClipping.IDClipping, itemDeClipping.Titulo, categoriaDoItem, itemDeClipping.Veiculo, itemDeClipping.DataPublicacao, itemDeClipping.Grupo, Clippings.IndexOf(itemDeClipping), itemDeClipping.CategoriaNumPrincipal, itemDeClipping.Upload, itemDeClipping.idUpload, itemDeClipping.Ordem);

                            listaDeItens.Add(item);
                        }
                    }

                    listaDeItens = (from itemDeLista in listaDeItens orderby itemDeLista.CategNum ascending, itemDeLista.Categoria ascending, itemDeLista.Upload descending, itemDeLista.idUpload descending, itemDeLista.Ordem ascending select itemDeLista).ToList();

                    for (int i = 1; i < listaDeItens.Count; i++)
                        listaDeItens[i - 1].Sequencia = i;

                    Session[SESSION_SELECIONADOS] = listaDeItens;
                }

                return (List<ItemSelecionado>)Session[SESSION_SELECIONADOS];
            }
            set
            {
                Session[SESSION_SELECIONADOS] = value;
            }
        }

        public int DestinatarioSelecionado
        {
            get
            {
                int destinatarioSelecionado = 0;

                if (Int32.TryParse(rblDestinatarios.SelectedValue, out destinatarioSelecionado))
                {
                    switch (destinatarioSelecionado)
                    {
                        case DESTINATARIO_CC:
                            return DESTINATARIO_CC;
                        case DESTINATARIO_CCO:
                            return DESTINATARIO_CCO;
                        default:
                            return DESTINATARIO_PARA;
                    }
                }

                return DESTINATARIO_PARA;
            }
        }

        public System.Collections.Generic.KeyValuePair<int, int> GrupoEnvioEmails
        {
            get
            {
                if (Session[SESSION_EMAILS] == null)
                {
                    Session[SESSION_EMAILS] = new System.Collections.Generic.KeyValuePair<int, int>();
                }

                return (System.Collections.Generic.KeyValuePair<int, int>)Session[SESSION_EMAILS];
            }
            set
            {
                Session[SESSION_EMAILS] = value;
            }
        }

        public string Formato
        {
            get
            {
                return Request.QueryString[QUERYSTRING_FORMATO] != null ? Request.QueryString[QUERYSTRING_FORMATO] : FORMATO_MAIL;
            }
        }

        public string Selecao
        {
            get
            {
                return ddlExportar != null && ddlExportar.SelectedItem != null && !String.IsNullOrWhiteSpace(ddlExportar.SelectedItem.Text) ? ddlExportar.SelectedItem.Text : Formato;
            }
        }

        public bool ServicoOnline
        {
            get
            {
                return !appSession.EmpresaAtiva.ServicoEstaOffline;
            }
        }

        public bool isSimples
        {
            get
            {
                if (Session["IS_SIMPLES"] == null)
                {
                    Session["IS_SIMPLES"] = false;
                }

                return (bool)Session["IS_SIMPLES"];
            }
            set
            {
                Session["IS_SIMPLES"] = value;
            }
        }

        #endregion

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            base.PageBaseSetEvents(this.Master);

            if (!IsPostBack)
            {
                ClearSession();

                if (!appSession.EmpresaAtiva.FoiEnriquecido)
                    appSession.EmpresaAtiva = uiAdmin.GetEmpresaCamposPersonalizados(appSession.EmpresaAtiva);

                Clippings = null;
                Details = null;
                Analysis = null;

                rptExport.DataSource = this.ListaCampos;
                rptExport.DataBind();

                lblTitulo.Text = Mensagem;
                lblQtd.Text = String.Format("Quantidade de clippings: {0}", appSession.ClippingsSelecao.IdClipping_ToExport.Count);

                switch (this.Formato)
                {
                    case FORMATO_CSV:
                        if (!VerifyPermission((int)Business.AcoesList.iPortal_DisponibilizarClipping_DisponibilizarClippingCSV))
                            Response.Redirect("~/Portal.aspx");
                        break;
                    case FORMATO_PDF:
                        if (!VerifyPermission((int)Business.AcoesList.iPortal_DisponibilizarClipping_DisponibilizarClippingPDF))
                            Response.Redirect("~/Portal.aspx");
                        pnlOpcoesPDF.Visible = true;
                        btnSimples.Visible = true;
                        break;
                    case FORMATO_XLSX:
                        if (!VerifyPermission((int)Business.AcoesList.iPortal_DisponibilizarClipping_DisponibilizarClippingXLSX))
                            Response.Redirect("~/Portal.aspx");
                        break;
                    case FORMATO_DOCX:
                        if (!VerifyPermission((int)Business.AcoesList.iPortal_DisponibilizarClipping_DisponibilizarClippingDOCX))
                            Response.Redirect("~/Portal.aspx");
                        break;
                    case FORMATO_MAIL:
                        if (!VerifyPermission((int)Business.AcoesList.iPortal_DisponibilizarClipping_DisponibilizarClippingEMail))
                            Response.Redirect("~/Portal.aspx");
                        pnlDestinatarios.Visible = true;
                        pnlOpcoes.Visible = true;
                        pnlSelecionados.Visible = true;
                        /* (2011-03-21) fulano - Evento foi para tela ExportExibirClipping */
#if (VERSIONOLD)
                        grdSelecionados.Visible = true;
#endif
                        /* (2011-03-21) fulano - fim */
                        btnDuo.Text = "Enviar Email";
                        btnExportar.Text = "Enviar Email";
                        pnlFrom.Visible = true;
                        ddlFormatos.Items.Clear();

                        chkMeuEmail.Text = string.Format("Enviar c�pia para meu email ({0}).", appSession.UsuarioAtivo.Email);

                        var mailExportDefaultFile = ConfigurationManager.AppSettings["Mail_Export_File_Default"];

                        if (File.Exists(mailExportDefaultFile))
                            ddlFormatos.Items.Add(new ListItem { Text = "Padr�o", Value = mailExportDefaultFile });

                        var mailExportGroupFile = System.Configuration.ConfigurationManager.AppSettings["Mail_Export_File_Grupo"];

                        if (File.Exists(mailExportGroupFile))
                            ddlFormatos.Items.Add(new ListItem { Text = "Padr�o Agrupamento", Value = mailExportGroupFile });

                        var mailExportDefaultFileLink = ConfigurationManager.AppSettings["Mail_Export_File_Default_Link"];

                        if (File.Exists(mailExportDefaultFileLink))
                            ddlFormatos.Items.Add(new ListItem { Text = "Padr�o Link", Value = mailExportDefaultFileLink });

                        var mailExportGroupFileLink = System.Configuration.ConfigurationManager.AppSettings["Mail_Export_File_Grupo_Link"];

                        if (File.Exists(mailExportGroupFileLink))
                            ddlFormatos.Items.Add(new ListItem { Text = "Padr�o Agrupamento Link", Value = mailExportGroupFileLink });
                        if (appSession.EmpresaAtiva.TemplatesEmail != null)
                        {
                            foreach (var template in appSession.EmpresaAtiva.TemplatesEmail)
                            {
                                ddlFormatos.Items.Add(new ListItem { Text = template.Key, Value = template.Value });
                            }

                            ddlFormatos.SelectedIndex = 0;
                        }

                        btnVerEmail.Enabled = ddlFormatos.Items.Count > 0;

                        /* (2011-03-21) fulano - Evento foi para tela ExportExibirClipping */
#if (VERSIONOLD)
                        grdSelecionados.DataSource = DadosSelecionados;
                        grdSelecionados.DataBind();
#endif
                        /* (2011-03-21) fulano - fim */

                        grdLista.DataSource = GruposDeContatos;
                        grdLista.DataBind();

                        break;
                }

            }

            if (!IsPostBack) ConfigureDropDown();
        }

        #endregion

        #region Events
        protected void btnListaEmails_Click(object sender, EventArgs e)
        {
            grdLista.EditIndex = -1;
            grdLista.DataSource = GruposDeContatos;
            grdLista.DataBind();
            modalPopupExtenderListas.Show();
        }

        protected void rptExport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CheckBox chkItemExport = (CheckBox)e.Item.FindControl("chkItemExport");
            Entities.ExportItem dataItem = (Entities.ExportItem)e.Item.DataItem;

            bool isChecked = true;
            bool isEnabled = true;
            string tooltip = String.Empty;

            if (chkItemExport != null && dataItem != null)
            {
                if (dataItem.Pai.Equals("ClippingAnalise"))
                {
                    if (this.ServicoOnline)
                    {
                        if (dataItem.Indice == 0)
                        {
                            isChecked = true;
                            isEnabled = true;
                        }
                        else
                        {
                            isChecked = VerifyPermission(dataItem.Indice);
                            isEnabled = VerifyPermission(dataItem.Indice);

                            if (!isChecked && !isEnabled)
                            {
                                tooltip = "Voc� n�o tem permiss�o para exportar este item.";
                            }

                        }
                    }
                    else
                    {
                        isChecked = false;
                        isEnabled = false;
                        tooltip = "N�o foi poss�vel carregar os dados de an�lise (Servi�o de clippings offline indispon�vel).";
                    }
                }
                else
                {
                    isChecked = true;
                    isEnabled = true;

                    if (Formato.Equals(FORMATO_MAIL) && dataItem.Indice != 0)
                    {
                        isEnabled = false;
                        tooltip = "Voc� n�o pode desativar o envio destes campos no email.";
                    }
                }

                chkItemExport.Checked = isChecked;
                chkItemExport.Enabled = isEnabled;
                chkItemExport.ToolTip = tooltip;
                chkItemExport.Text = dataItem.Desc;
            }

        }

        protected void rblDestinatarios_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (DestinatarioSelecionado)
            {
                case DESTINATARIO_CC:
                    txtDestinatarios.Visible = false;
                    txtCC.Visible = true;
                    txtCCO.Visible = false;
                    break;
                case DESTINATARIO_CCO:
                    txtDestinatarios.Visible = false;
                    txtCC.Visible = false;
                    txtCCO.Visible = true;
                    break;
                default:
                    txtDestinatarios.Visible = true;
                    txtCC.Visible = false;
                    txtCCO.Visible = false;
                    break;
            }

        }

        protected void grdLista_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            int rowIndex = 0;
            int idGrupoContato = 0;

            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex == grdLista.EditIndex)
            {
                rowIndex = e.Row.RowIndex;

                try
                {
                    idGrupoContato = (int)grdLista.DataKeys[rowIndex][0];
                }
                catch
                {
                    idGrupoContato = 0;
                }

                if (idGrupoContato > 0)
                {
                    List<Entities.Contato> contatosDoGrupo = TodosOsContatos.FindAll(contato => contato.IDGrupoContato == idGrupoContato);

                    if (contatosDoGrupo != null && contatosDoGrupo.Count > 0)
                    {
                        GridView grdContatos = (GridView)e.Row.FindControl("grdContatos");

                        if (grdContatos != null)
                        {

                            grdContatos.DataSource = contatosDoGrupo;
                            grdContatos.DataBind();

                        }
                    }
                }
            }
        }

        protected void grdLista_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

        }

        protected void imgUpGrupo_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgUpGrupo = sender as ImageButton;

            grdLista.EditIndex = -1;
            grdLista.DataSource = GruposDeContatos;
            grdLista.DataBind();

            modalPopupExtenderListas.Show();
        }

        protected void imgDownGrupo_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgDownGrupo = sender as ImageButton;

            if (imgDownGrupo != null)
            {
                int rowIndex = 0;

                if (Int32.TryParse(imgDownGrupo.CommandArgument, out rowIndex))
                {
                    if (grdLista != null && grdLista.Rows != null && grdLista.Rows.Count > rowIndex)
                    {
                        grdLista.EditIndex = rowIndex;
                        grdLista.DataSource = GruposDeContatos;
                        grdLista.DataBind();
                    }
                }
            }

            modalPopupExtenderListas.Show();
        }

        protected void imgContato_Click(object sender, ImageClickEventArgs e)
        {

            modalPopupExtenderListas.Hide();

            ImageButton imgContato = sender as ImageButton;

            int idContato = 0;

            if (imgContato != null)
            {
                if (Int32.TryParse(imgContato.CommandArgument, out idContato))
                {
                    Entities.Contato contatoSelecionado = TodosOsContatos.Find(contato => contato.IDContato == idContato);

                    if (contatoSelecionado != null)
                    {

                        string padrao = String.Format("{0}", contatoSelecionado.Email);
                        TextBoxDestinatario.Text = String.Format("{0}{1}", TextBoxDestinatario.Text, padrao);
                    }
                }
            }

        }

        protected void imgGrupo_Click(object sender, ImageClickEventArgs e)
        {

            modalPopupExtenderListas.Hide();

            ImageButton imgGrupo = sender as ImageButton;

            int idGrupoContato = 0;

            if (imgGrupo != null)
            {
                if (Int32.TryParse(imgGrupo.CommandArgument, out idGrupoContato))
                {
                    Entities.GrupoContato grupoSelecionado = GruposDeContatos.Find(grupo => grupo.IdGrupoContato == idGrupoContato);

                    if (grupoSelecionado != null)
                    {

                        string padrao = String.Format("[Lista({0}): {1}]", grupoSelecionado.IdGrupoContato, grupoSelecionado.Nome);

                        if (TextBoxDestinatario.Text.Trim().EndsWith(",") || String.IsNullOrWhiteSpace(TextBoxDestinatario.Text))
                        {
                            TextBoxDestinatario.Text = String.Format("{0}{1}", TextBoxDestinatario.Text, padrao);
                        }
                        else
                        {
                            TextBoxDestinatario.Text = String.Format("{0},{1}", TextBoxDestinatario.Text, padrao);
                        }

                    }
                }
            }

            //modalPopupExtenderListas.Show();
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            bool eVisualizacao = Selecao.Equals(FORMATO_MAIL) && sender is Button && ((Button)sender).ID.Equals("btnVerEmail");

            if (!eVisualizacao && !Page.IsValid) return;

            /* (2011-03-21) fulano - N�o precisa atualizar vari�vel Clippings
             * Essa rotina replica o clipping que est�o para mais de uma categoria
             */
            /*
            if (Selecao == FORMATO_MAIL)
            {
                var tmp = new List<Business.Entities.Clipping>();

                foreach (var data in DadosSelecionados)
                {
                    var clipping = Clippings.Find(c => c.IDClipping == data.IDClipping);
                    clipping.Sequencia = data.Sequencia;
                    tmp.Add(clipping);
                }

                Clippings = tmp;
            }
            */
            /* (2011-03-21) fulano - fim */


            // verifica se a op��o escolhida foi booklet simplificado
            isSimples = Selecao.Equals(FORMATO_PDF) && sender is Button && ((Button)sender).ID.Equals("btnSimples");

            // valores dos campos definidos na camada de componentes
            List<Entities.ExportItem> lstCampos = this.ListaCampos;

            // caso seja booklet simplificado
            if (isSimples)
            {
                rdlGrupo.SelectedValue = "1";
                // filtra a lista de campos com apenas os campos padr�o
                lstCampos = lstCampos.FindAll(i => i.Simples == true);

                rptExport.DataSource = this.ListaCampos;
                rptExport.DataBind();
            }

            // lista final que ter� os valores exportados
            List<Entities.ExportItem> lst = new List<Entities.ExportItem>();

            // percorre a lista de clippings selecionados
            foreach (Entities.Clipping c in Clippings)
            {
                int idClipping = c.IDClipping;

                c.Chave = Entities.ClippingRedirect.Create(appSession.EmpresaAtiva.IdEmpresa, idClipping, appSession.PrazoExpiracaoEmDias, Entities.RedirectTo.Clipping, Entities.RedirectFrom.export_aspx, true);
                c.URL = URL;

                if (Selecao.Equals(FORMATO_MAIL))
                {
                    c.PDFKey = c.TemArquivo ? Entities.ClippingRedirect.Create(appSession.EmpresaAtiva.IdEmpresa, idClipping, appSession.PrazoExpiracaoEmDias, Entities.RedirectTo.ArquivoOrigem, Entities.RedirectFrom.email_aspx, true) : "";
                }
                else
                {
                    c.PDFKey = c.TemArquivoPDF ? Entities.ClippingRedirect.Create(appSession.EmpresaAtiva.IdEmpresa, idClipping, appSession.PrazoExpiracaoEmDias, Entities.RedirectTo.ArquivoOrigem, Entities.RedirectFrom.export_aspx, true) : "";
                }

                c.UrlClipping = String.Format("{0}{1}?id={2}", c.URL, "Login.aspx", Entities.ClippingRedirect.Create(appSession.EmpresaAtiva.IdEmpresa, idClipping, 0, Entities.RedirectTo.Clipping, Entities.RedirectFrom.export_aspx, false));
                c.UrlPdf = String.Format("{0}{1}?id={2}", c.URL, "LerArquivo.aspx", Entities.ClippingRedirect.Create(appSession.EmpresaAtiva.IdEmpresa, idClipping, appSession.PrazoExpiracaoEmDias, Entities.RedirectTo.ArquivoOrigem, Entities.RedirectFrom.export_aspx, false));

                if (c.Categoria == null || !(c.Categoria.Count > 0))
                {
                    c.Categoria = new List<string>();
                    c.Categoria.Add("Sem categoria");
                }

                // clipping detail
                Entities.ClippingDetail clippingDetail = Details.Find(i => i.IDClipping == idClipping);
                if (clippingDetail == null)
                {
                    clippingDetail = new Entities.ClippingDetail();
                    clippingDetail.IDClipping = idClipping;
                }

                // clipping an�lise
                Entities.ClippingAnalise ca = Analysis.Find(i => i.IdClipping == idClipping);
                if (ca == null)
                {
                    ca = new Entities.ClippingAnalise();
                    ca.IdClipping = idClipping;
                }
                // obt�m os valores de exportar 

                // clipping
                List<Entities.ExportItem> lstC = c.Exportar();
                // clipping detail
                List<Entities.ExportItem> lstCD = clippingDetail.Exportar();
                // clipping an�lise
                List<Entities.ExportItem> lstCA = ca.Exportar();

                // percorre o repeater a procura de itens selecionados
                foreach (System.Web.UI.WebControls.RepeaterItem ri in rptExport.Items)
                {
                    // obt�m os controles associados

                    // ID do campo
                    System.Web.UI.WebControls.HiddenField hdnItemExport = (System.Web.UI.WebControls.HiddenField)ri.FindControl("hdnItemExport");
                    // Sele��o do campo
                    System.Web.UI.WebControls.CheckBox chkItemExport = (System.Web.UI.WebControls.CheckBox)ri.FindControl("chkItemExport");
                    // Simples
                    System.Web.UI.WebControls.HiddenField hdnItemSimples = (System.Web.UI.WebControls.HiddenField)ri.FindControl("hdnItemSimples");

                    // obt�m o ID
                    string ID = hdnItemExport.Value.ToString();
                    // controle de book simples
                    bool itemSimples = false;

                    // cria um predicado b�sico que ser� usado para buscar os itens de export��o
                    Predicate<Entities.ExportItem> predicate = new Predicate<Entities.ExportItem>(delegate (Entities.ExportItem ei)
                    {
                        // o ID do item tem de ser igual ao ID da linha do repeater
                        return ei.ID.Equals(ID);
                    });

                    // seleciona ou n�o de acordo com a simplicidade
                    if (isSimples)
                    {
                        if (Boolean.TryParse(hdnItemSimples.Value, out itemSimples) && itemSimples)
                        {
                            chkItemExport.Checked = true;
                        }
                        else
                        {
                            chkItemExport.Checked = false;
                        }
                    }

                    // verifica se o item foi selecionado e se por algum motivo o ID n�o � vazio
                    if (chkItemExport.Checked == true && String.IsNullOrEmpty(ID.Trim()) == false)
                    {
                        // se predicado existir tanto na lista de campos
                        // como na lista de valores das entidades
                        // faz-se um merge de dados e adiciona-se na lista final

                        // clipping
                        if (lstC.Exists(predicate) && lstCampos.Exists(predicate))
                        {
                            // obt�m os itens que satisfazem a condi��o
                            List<Entities.ExportItem> lst_exC = lstC.FindAll(predicate);
                            // obt�m o �nico item que satisfaz a condi��o
                            Entities.ExportItem exCF = lstCampos.Find(predicate);

                            if (Fields.Exists(predicate) == false) Fields.Add(exCF);

                            // percorre todos itens encontrados fazendo merge com os dados do campo
                            foreach (Entities.ExportItem exC in lst_exC)
                            {
                                lst.Add(new Entities.ExportItem(ID, exCF.Tipo, exC.Valor, exCF.Ordem, exC.Indice, exCF.Pai, exCF.Desc, idClipping));
                            }
                        }

                        // clipping detail
                        if (lstCD.Exists(predicate) && lstCampos.Exists(predicate))
                        {
                            // obt�m os itens que satisfazem a condi��o
                            List<Entities.ExportItem> lst_exCD = lstCD.FindAll(predicate);
                            // obt�m o �nico item que satisfaz a condi��o
                            Entities.ExportItem exCDF = lstCampos.Find(predicate);

                            if (Fields.Exists(predicate) == false) Fields.Add(exCDF);

                            // percorre todos itens encontrados fazendo merge com os dados do campo
                            foreach (Entities.ExportItem exCD in lst_exCD)
                            {
                                lst.Add(new Entities.ExportItem(ID, exCDF.Tipo, exCD.Valor, exCDF.Ordem, exCD.Indice, exCDF.Pai, exCDF.Desc, idClipping));
                            }
                        }

                        // clipping an�lise
                        if (lstCA.Exists(predicate) && lstCampos.Exists(predicate))
                        {
                            // obt�m os itens que satisfazem a condi��o
                            List<Entities.ExportItem> lst_exCA = lstCA.FindAll(predicate);
                            // obt�m o �nico item que satisfaz a condi��o
                            Entities.ExportItem exCAF = lstCampos.Find(predicate);

                            if (Fields.Exists(predicate) == false) Fields.Add(exCAF);

                            // percorre todos itens encontrados fazendo merge com os dados do campo
                            foreach (Entities.ExportItem exCA in lst_exCA)
                            {
                                string desc = exCAF.Trocar ? exCA.Alt : exCAF.Desc;


                                //if (String.IsNullOrEmpty(desc))
                                //{
                                //    desc = exCA.ID.Replace("ClippingAnalise.", "").Replace("ValueCampo", "CampoAdicional");
                                //}

                                lst.Add(new Entities.ExportItem(ID, exCAF.Tipo, exCA.Valor, exCAF.Ordem, exCA.Indice, exCAF.Pai, desc, idClipping));
                            }
                        }
                    }
                }
            }

            if (lst.Count <= 0)
            {
                if (this.Master is iPortal_User && !eVisualizacao)
                {
                    ((iPortal_User)this.Master).ShowMessage("Alerta", "� necess�rio a sele��o de pelo menos um atributo e um clipping para esta a��o");
                }
            }
            else
            {
                ExportData(lst, eVisualizacao, isSimples);

                if (IsServiceOffline)
                {
                    ((iPortal_User)this.Master).ShowMessage("Aviso", "N�o foi poss�vel exportar os dados de an�lise (Servi�o de clippings offline indispon�vel).");
                }
            }
        }

        protected void btnExibirClipping_Click(object sender, EventArgs e)
        {
            lblQtd.Text = String.Format("Quantidade de clippings: {0}", appSession.ClippingsSelecao.IdClipping_ToExport.Count);
        }

        #endregion

        #region Private Methods

        protected void ClearSession()
        {
            Session[SESSION_CONTATOS] = null;
            Session[SESSION_GRUPOS] = null;
            Session[SESSION_SELECIONADOS] = null;
        }

        protected void ConfigureDropDown()
        {

            ddlExportar.Items.Clear();

            switch (this.Formato)
            {
                case FORMATO_CSV:
                    ddlExportar.Items.Add(FORMATO_CSV);
                    ddlExportar.Items.Add(FORMATO_XLSX);
                    ddlExportar.Visible = true;
                    pnlType.Visible = true;
                    pnlSimples.Visible = false;
                    btnExportar.Visible = true;
                    break;
                case FORMATO_DOCX:
                    ddlExportar.Items.Add(FORMATO_DOCX);
                    ddlExportar.Visible = false;
                    pnlType.Visible = false;
                    pnlSimples.Visible = false;
                    btnExportar.Visible = true;
                    break;
                case FORMATO_MAIL:
                    ddlExportar.Items.Add(FORMATO_MAIL);
                    ddlExportar.Visible = false;
                    pnlType.Visible = false;
                    pnlSimples.Visible = false;
                    btnExportar.Visible = true;
                    break;
                case FORMATO_PDF:
                    ddlExportar.Items.Add(FORMATO_PDF);
                    ddlExportar.Visible = false;
                    pnlType.Visible = false;
                    pnlSimples.Visible = true;
                    btnExportar.Visible = false;
                    break;
                case FORMATO_XLSX:
                    ddlExportar.Items.Add(FORMATO_XLSX);
                    ddlExportar.Items.Add(FORMATO_CSV);
                    ddlExportar.Visible = true;
                    pnlType.Visible = true;
                    pnlSimples.Visible = false;
                    btnExportar.Visible = true;
                    break;
            }

            try
            {
                ddlExportar.SelectedIndex = 0;
            }
            catch
            {
                ddlExportar.ClearSelection();
            }
        }

        protected bool ContainsID(string ID)
        {
            // percorre o repeater a procura de itens selecionados
            foreach (System.Web.UI.WebControls.RepeaterItem ri in rptExport.Items)
            {
                // obt�m os controles associados

                // ID do campo
                System.Web.UI.WebControls.HiddenField hdnItemExport = (System.Web.UI.WebControls.HiddenField)ri.FindControl("hdnItemExport");
                // Sele��o do campo
                System.Web.UI.WebControls.CheckBox chkItemExport = (System.Web.UI.WebControls.CheckBox)ri.FindControl("chkItemExport");
                // Simples
                System.Web.UI.WebControls.HiddenField hdnItemSimples = (System.Web.UI.WebControls.HiddenField)ri.FindControl("hdnItemSimples");

                if (chkItemExport.Checked && hdnItemExport.Value.ToString().Contains(ID))
                {
                    return true;
                }
            }

            return false;
        }

        protected void ExportData(List<Entities.ExportItem> lst, bool eVisualizacao, bool isSimples)
        {
            switch (this.Selecao)
            {
                case FORMATO_CSV:
                    ExportCSV(lst);
                    break;
                case FORMATO_PDF:
                    ExportPDF(lst, isSimples);
                    break;
                case FORMATO_XLSX:
                    ExportXLS(lst);
                    break;
                case FORMATO_DOCX:
                    ExportDOC(lst);
                    break;
                case FORMATO_MAIL:
                    ExportMAIL(lst, eVisualizacao);
                    break;
            }
        }

        private Entities.ExportItem ItemByID(string ID, List<Entities.ExportItem> lst)
        {
            Predicate<Entities.ExportItem> pred = new Predicate<Entities.ExportItem>(delegate (Entities.ExportItem ei)
            {
                return ei.ID.Equals(ID);
            });

            if (lst.Exists(pred))
            {
                return lst.Find(pred);
            }

            return null;
        }

        private Business.Entities.Clipping CopyClipping(Business.Entities.Clipping item)
        {
            Business.Entities.Clipping newItem = new Business.Entities.Clipping();

            newItem.Agrupado = item.Agrupado;
            newItem.ArquivoOrigem = item.ArquivoOrigem;
            newItem.Autor = item.Autor;
            newItem.Categoria = item.Categoria;
            newItem.CategoriaNum = item.CategoriaNum;
            //newItem.CategoriaNumPrincipal = item.CategoriaNumPrincipal;
            //newItem.CategoriaPrincipal = item.CategoriaPrincipal;
            newItem.Chave = item.Chave;
            newItem.Cidade = item.Cidade;
            newItem.Coluna = item.Coluna;
            newItem.DataPublicacao = item.DataPublicacao;
            newItem.Disponibilizacao = item.Disponibilizacao;
            newItem.Empresa = item.Empresa;
            newItem.Estado = item.Estado;
            newItem.Expiracao = item.Expiracao;
            //newItem.Extensao = item.Extensao;
            newItem.Grupo = item.Grupo;
            newItem.IDClipping = item.IDClipping;
            newItem.idUpload = item.idUpload;
            newItem.Keyword = item.Keyword;
            newItem.Ordem = item.Ordem;
            newItem.Pagina = item.Pagina;
            newItem.Pai = item.Pai;
            newItem.Pasta = item.Pasta;
            newItem.PDFKey = item.PDFKey;
            newItem.Sequencia = item.Sequencia;
            //newItem.TemArquivo = item.TemArquivo;
            //newItem.TemArquivoNaoPDF = item.TemArquivoNaoPDF;
            //newItem.TemArquivoPDF = item.TemArquivoPDF;
            newItem.TipoVeiculo = item.TipoVeiculo;
            newItem.Titulo = item.Titulo;
            newItem.UmaVez = item.UmaVez;
            newItem.Upload = item.Upload;
            newItem.URL = item.URL;
            newItem.UrlClipping = item.UrlClipping;
            newItem.UrlPdf = item.UrlPdf;
            newItem.Veiculo = item.Veiculo;
            //newItem.VerImagemCaminho = item.VerImagemCaminho;
            //newItem.VerImagemTooltip = item.VerImagemTooltip;

            return newItem;

        }

        #region Export
        /// <summary>
        /// Exporta uma lista de itens para o formato PDF
        /// </summary>
        /// <param name="lst"></param>
        protected void ExportPDF(List<Entities.ExportItem> lst, bool simplificado)
        {
            if (simplificado == true)
            {
                System.IO.MemoryStream ms = (MemoryStream)uiAdmin.GetBookSimplificado(appSession.UsuarioAtivo.IdUsuario, appSession.EmpresaAtiva.IdEmpresa, Business.TipoLeitura.ClippingDetail, Clippings, Details, Analysis);

                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.ContentType = "application/pdf";
                context.Response.ContentEncoding = Encoding.Default;
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=OBC_Clippings.pdf");
                context.Response.AppendHeader("Content-Length", ms.Length.ToString());
                context.Response.Flush();
                context.Response.BinaryWrite(ms.ToArray());
                context.Response.Flush();
            }


            if (rdlGrupo.SelectedValue.Equals("1") && simplificado == false)
            {

                System.IO.MemoryStream ms = (MemoryStream)uiAdmin.GetClipping(appSession.UsuarioAtivo.IdUsuario, appSession.EmpresaAtiva.IdEmpresa, Business.TipoLeitura.ClippingDetail, lst, appSession.EmpresaAtiva.ImagemCabecalho, Clippings, Details, Analysis);

                HttpContext context = HttpContext.Current;
                context.Response.Clear();
                context.Response.ContentType = "application/pdf";
                context.Response.ContentEncoding = Encoding.Default;
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=OBC_Clippings.pdf");
                context.Response.AppendHeader("Content-Length", ms.Length.ToString());
                context.Response.Flush();
                context.Response.BinaryWrite(ms.ToArray());
                context.Response.Flush();
            }

            if (rdlGrupo.SelectedValue.Equals("2") && simplificado == false)
            {
                long lenght = 0;
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    // percorre a lista de clippings selecionados
                    foreach (int idClipping in appSession.ClippingsSelecao.IdClipping_ToExport)
                    {
                        System.IO.MemoryStream ms = (MemoryStream)uiAdmin.GetClipping(appSession.UsuarioAtivo.IdUsuario, appSession.EmpresaAtiva.IdEmpresa, Business.TipoLeitura.ClippingDetail, lst, appSession.EmpresaAtiva.ImagemCabecalho, Clippings.FindAll(i => i.IDClipping == idClipping), Details.FindAll(i => i.IDClipping == idClipping), Analysis.FindAll(i => i.IdClipping == idClipping));

                        lenght += ms.Length;

                        zip.AddEntry(idClipping.ToString() + ".pdf", ms.ToArray());
                    }
                    System.IO.MemoryStream m = new MemoryStream();
                    zip.Save(m);
                    HttpContext context = HttpContext.Current;
                    context.Response.Clear();
                    context.Response.ContentType = "application/zip";
                    context.Response.ContentEncoding = Encoding.Default;
                    context.Response.AppendHeader("Content-Disposition", "attachment; filename=OBC_Clippings.zip");
                    context.Response.AppendHeader("Content-Length", m.Length.ToString());
                    context.Response.Flush();
                    context.Response.BinaryWrite(m.ToArray());
                    context.Response.Flush();
                }
            }
        }

        /// <summary>
        /// Exporta uma lista de itens para o formato DOC
        /// </summary>
        /// <param name="lst"></param>
        protected void ExportDOC(List<Entities.ExportItem> lst)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            ExportadorDoc exp = new ExportadorDoc();

            exp.CreatePackage(ref ms, Server.MapPath(@"~\Images\clients\" + appSession.EmpresaAtiva.ImagemCabecalho), lst, Clippings, Details, Analysis, Server);

            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            context.Response.ContentEncoding = Encoding.Default;
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=OBC_Clippings.docx");
            context.Response.AppendHeader("Content-Length", ms.Length.ToString());
            //context.Response.Buffer = true;
            //context.Response.BufferOutput = true;
            context.Response.Flush();
            context.Response.BinaryWrite(ms.GetBuffer());
            context.Response.Flush();
            //context.Response.End();
        }

        /// <summary>
        /// Exporta uma lista de itens para o formato MAIL
        /// </summary>
        /// <param name="lst"></param>
        protected void ExportMAIL(List<Entities.ExportItem> lst, bool eVisualizacao)
        {

            bool umaVez = chkUmaVez.Checked;
            bool agrupar = chkAgrupar.Checked;

            List<Entities.Clipping> listaDeClippings = new List<Entities.Clipping>();

            Entities.Clipping clipping = new Entities.Clipping();

            List<string> listaDeCategorias = new List<string>();

            List<int> lista = new List<int>();

            /*Trabalha sempre com uma c�pia pois caso o usu�rio altere chkUmaVez consegue-se pegar o estado original*/
            List<Entities.Clipping> ClippingsTmp = new List<Entities.Clipping>();
            foreach (var ic in Clippings)//.OrderBy(p=>p.CategoriaPrincipal, p.Sequencia))
            {
                ClippingsTmp.Add(CopyClipping(ic));
            }

            foreach (Entities.Clipping itemDeClipping in ClippingsTmp)
            {
                //N�o permite replicar registro com o mesmo IDClipping (Se o Clipping pertencer a mais de uma categoria, os items ser�o inseridos junto com o primeiro)  
                if (listaDeClippings.Where(lc => lc.IDClipping == itemDeClipping.IDClipping).Count() > 0) continue;

                bool umaVezAux = umaVez;
                //Se foi selecionado para aparecer apenas 1 vez e,
                //o clipping est� associado a mais de uma categoria e,
                //existe um clipping que foi ordenado manualmente. Ent�o n�o deve aparecer apenas 1 vez
                if (umaVezAux && itemDeClipping.Categoria.Count() > 1 && ClippingsTmp.Where(p => p.IDClipping == itemDeClipping.IDClipping && p.Grupo != 0).Count() > 0)
                    umaVezAux = false;

                if (umaVezAux)
                {//Quando for para aparecer apenas 1 vez, n�o pode duplicar os clipping que perten�am a mais de uma categoria

                    clipping = new Entities.Clipping();

                    clipping.ArquivoOrigem = itemDeClipping.ArquivoOrigem;
                    clipping.Autor = itemDeClipping.Autor;
                    clipping.Chave = itemDeClipping.Chave;
                    clipping.Cidade = itemDeClipping.Cidade;
                    clipping.Coluna = itemDeClipping.Coluna;
                    clipping.DataPublicacao = itemDeClipping.DataPublicacao;
                    clipping.Disponibilizacao = itemDeClipping.Disponibilizacao;
                    clipping.Empresa = itemDeClipping.Empresa;
                    clipping.Estado = itemDeClipping.Estado;
                    clipping.Expiracao = itemDeClipping.Expiracao;
                    clipping.IDClipping = itemDeClipping.IDClipping;
                    clipping.Keyword = itemDeClipping.Keyword;
                    clipping.Pagina = itemDeClipping.Pagina;
                    clipping.Pasta = itemDeClipping.Pasta;
                    clipping.PDFKey = itemDeClipping.PDFKey;
                    clipping.TipoVeiculo = itemDeClipping.TipoVeiculo;
                    clipping.Titulo = itemDeClipping.Titulo;
                    clipping.URL = itemDeClipping.URL;
                    clipping.UrlClipping = itemDeClipping.UrlClipping;
                    clipping.UrlPdf = itemDeClipping.UrlPdf;
                    clipping.Veiculo = itemDeClipping.Veiculo;
                    clipping.Grupo = itemDeClipping.Grupo;
                    clipping.Sequencia = itemDeClipping.Sequencia;
                    clipping.Pai = String.Empty;
                    clipping.Agrupado = true;

                    clipping.Upload = itemDeClipping.Upload;
                    clipping.idUpload = itemDeClipping.idUpload;
                    clipping.Ordem = itemDeClipping.Ordem;

                    clipping.Categoria = new List<string>();
                    clipping.Categoria.Add(itemDeClipping.CategoriaPrincipal);
                    clipping.CategoriaNum.Add(itemDeClipping.CategoriaNumPrincipal);

                    if (agrupar)
                    {
                        /*(2011-03-21) fulano - Agrupa por Titulo (Automaticamente)*/
                        if (!lista.Contains(itemDeClipping.IDClipping))
                        {//Clipping ainda n�o foi agrupado

                            if (itemDeClipping.Grupo == 0)
                            {//Agrupamento manual � priorit�rio
                                itemDeClipping.Grupo = itemDeClipping.IDClipping;
                                var qc = ClippingsTmp.Where(lc =>
                                                                lc.IDClipping != itemDeClipping.IDClipping &&
                                                                (lc.Grupo == 0 || lc.Grupo == lc.IDClipping) &&
                                                                lc.Titulo.ToLower(new System.Globalization.CultureInfo("pt-BR")) == itemDeClipping.Titulo.Trim().ToLower(new System.Globalization.CultureInfo("pt-BR"))
                                                                );
                                foreach (var item in qc)
                                {//Pega todos os clipping com mesmo t�tulo e agrupa (atributo "Grupo")
                                    clipping.Grupo = itemDeClipping.IDClipping;
                                    item.Grupo = itemDeClipping.IDClipping;
                                    lista.Add(item.IDClipping);
                                }
                            }
                            //else
                            //{
                            //    //Considerar Adrupamento Manual
                            //    clipping.Grupo = itemDeClipping.Grupo;
                            //}
                        }
                        //else
                        //{
                        //    //Considerar Adrupamento Manual
                        //    clipping.Grupo = itemDeClipping.Grupo;
                        //}
                    }
                    //else
                    //{
                    //    //Considerar Adrupamento Manual
                    //    clipping.Grupo = itemDeClipping.Grupo;
                    //}
                    listaDeClippings.Add(clipping);
                }
                else
                {

                    foreach (string categoria in itemDeClipping.Categoria)
                    {
                        clipping = new Entities.Clipping();

                        clipping.ArquivoOrigem = itemDeClipping.ArquivoOrigem;
                        clipping.Autor = itemDeClipping.Autor;
                        clipping.Chave = itemDeClipping.Chave;
                        clipping.Cidade = itemDeClipping.Cidade;
                        clipping.Coluna = itemDeClipping.Coluna;
                        clipping.DataPublicacao = itemDeClipping.DataPublicacao;
                        clipping.Disponibilizacao = itemDeClipping.Disponibilizacao;
                        clipping.Empresa = itemDeClipping.Empresa;
                        clipping.Estado = itemDeClipping.Estado;
                        clipping.Expiracao = itemDeClipping.Expiracao;
                        clipping.IDClipping = itemDeClipping.IDClipping;
                        clipping.Keyword = itemDeClipping.Keyword;
                        clipping.Pagina = itemDeClipping.Pagina;
                        clipping.Pasta = itemDeClipping.Pasta;
                        clipping.PDFKey = itemDeClipping.PDFKey;
                        clipping.TipoVeiculo = itemDeClipping.TipoVeiculo;
                        clipping.Titulo = itemDeClipping.Titulo;
                        clipping.URL = itemDeClipping.URL;
                        clipping.UrlClipping = itemDeClipping.UrlClipping;
                        clipping.UrlPdf = itemDeClipping.UrlPdf;
                        clipping.Veiculo = itemDeClipping.Veiculo;

                        var icateg = ClippingsTmp.Where(p => p.IDClipping == itemDeClipping.IDClipping && p.CategoriaPrincipal.ToLower(new System.Globalization.CultureInfo("pt-BR")) == categoria.ToLower(new System.Globalization.CultureInfo("pt-BR"))).FirstOrDefault();
                        if (icateg != null)
                        {
                            clipping.Sequencia = icateg.Sequencia;
                            clipping.Grupo = icateg.Grupo;
                        }
                        else
                        {
                            clipping.Sequencia = itemDeClipping.Sequencia;
                            clipping.Grupo = itemDeClipping.Grupo;
                        }
                        clipping.Upload = itemDeClipping.Upload;
                        clipping.idUpload = itemDeClipping.idUpload;
                        clipping.Ordem = itemDeClipping.Ordem;

                        clipping.Pai = String.Empty;
                        clipping.Agrupado = false;

                        clipping.Categoria = new List<string>();
                        clipping.Categoria.Add(categoria);
                        clipping.CategoriaNum = new List<int>();

                        if (itemDeClipping.CategoriaNum.Count != 0)
                            clipping.CategoriaNum.Add(itemDeClipping.CategoriaNum[itemDeClipping.Categoria.IndexOf(categoria)]);
                        else
                            clipping.CategoriaNum.Add(-1);

                        bool BreakLoop = true;
                        if (agrupar)
                        {
                            if (!lista.Contains(itemDeClipping.IDClipping))
                            {//Clipping ainda n�o foi agrupado
                                //Clipping n�o possui agrupamento manual (em nenhuma das categorias)
                                BreakLoop = (itemDeClipping.Grupo == 0) ||
                                            ClippingsTmp.Where(lc =>
                                                                lc.IDClipping == itemDeClipping.IDClipping &&
                                                                (lc.Grupo != 0) &&
                                                                lc.Titulo.ToLower(new System.Globalization.CultureInfo("pt-BR")) == itemDeClipping.Titulo.Trim().ToLower(new System.Globalization.CultureInfo("pt-BR"))
                                                                ).Count() == 0;

                                /*(2011-03-21) fulano - Agrupa por Titulo (Automaticamente)*/
                                if (itemDeClipping.Grupo == 0)
                                {
                                    //Agrupamento manual � priorit�rio
                                    itemDeClipping.Grupo = itemDeClipping.IDClipping;

                                    var qc = ClippingsTmp.Where(lc =>
                                                                lc.IDClipping != itemDeClipping.IDClipping &&
                                                                (lc.Grupo == 0 || lc.Grupo == lc.IDClipping) &&
                                                                lc.Titulo.ToLower(new System.Globalization.CultureInfo("pt-BR")) == itemDeClipping.Titulo.Trim().ToLower(new System.Globalization.CultureInfo("pt-BR"))
                                                                );
                                    foreach (var item in qc)
                                    {
                                        clipping.Grupo = itemDeClipping.IDClipping;
                                        item.Grupo = itemDeClipping.IDClipping;
                                        lista.Add(item.IDClipping);
                                    }
                                }
                                //else
                                //{
                                //    //Considerar Adrupamento Manual
                                //    clipping.Grupo = itemDeClipping.Grupo;
                                //}
                                /*(2011-03-21) fulano - fim*/
                            }
                            //else
                            //{//Considerar Adrupamento Manual
                            //    clipping.Grupo = itemDeClipping.Grupo;
                            //}
                        }
                        //else
                        //{//Considerar Adrupamento Manual
                        //    clipping.Grupo = itemDeClipping.Grupo;
                        //}
                        listaDeClippings.Add(clipping);

                        /*(2011-03-21) fulano - N�o � para replicar por categoria no "Agrupar Autom�tico" */
                        if (agrupar && BreakLoop) break;
                        /*(2011-03-21) fulano - fim */
                    }
                }
            }

            ExportadorMail exp = new ExportadorMail();

#if DEBUG
            lst.Add(new Entities.ExportItem("Clipping.IDClipping", typeof(string), "", 0, 100, "Clipping", "IDClipping", 0));
#endif
            lst.Add(new Entities.ExportItem("Clipping.Grupo", typeof(string), "", 0, 100, "Clipping", "Grupo", 0));
            lst.Add(new Entities.ExportItem("Clipping.Sequencia", typeof(string), "", 0, 200, "Clipping", "Sequencia", 0));
            lst.Add(new Entities.ExportItem("Clipping.Pai", typeof(string), "", 0, 300, "Clipping", "Pai", 0));
            lst.Add(new Entities.ExportItem("Clipping.VerImagemTooltip", typeof(string), "", 0, 400, "Clipping", "VerImagemTooltip", 0));
            lst.Add(new Entities.ExportItem("Clipping.VerImagemCaminho", typeof(string), "", 0, 500, "Clipping", "VerImagemCaminho", 0));

            exp.Create(Request, appSession, lst, ddlFormatos.SelectedValue, listaDeClippings, Details, Server);

            if (!eVisualizacao)
            {
                bool sentAll = true;

                if ((ListaDeDestinatarios == null || ListaDeDestinatarios.ToList().Count <= 0) &&
                    (ListaDeCC == null || ListaDeCC.ToList().Count <= 0) &&
                    (ListaDeCCO == null || ListaDeCCO.ToList().Count <= 0))
                {
                    if (this.Master is iPortal_User)
                        ((iPortal_User)this.Master).ShowMessage("Alerta - E-Mail n�o enviado", "� necess�rio digitar pelo menos um destinat�rio no campo 'Para', 'Com C�pia' ou 'Com C�pia Oculta'.");
                }
                else if (appSession.UsuarioAtivo.IdTipoUsuario == OBC.iPortal.Business.TipoUsuario.OBC && QtdItemsListaDeDestinatarios > 10)
                {
                    if (this.Master is iPortal_User)
                        ((iPortal_User)this.Master).ShowMessage("Alerta - E-Mail n�o enviado", "� permitido apenas 10 endere�os de e-mail no campo 'Para', atualmente tem " + ListaDeDestinatarios.ToList().Count().ToString() + ".");
                }
                else if (appSession.UsuarioAtivo.IdTipoUsuario == OBC.iPortal.Business.TipoUsuario.OBC && QtdItemsListaDeCC > 10)
                {
                    if (this.Master is iPortal_User)
                        ((iPortal_User)this.Master).ShowMessage("Alerta - E-Mail n�o enviado", "� permitido apenas 10 endere�os de e-mail no campo 'Com C�pia', atualmente tem " + ListaDeCC.ToList().Count().ToString() + ".");
                }
                else if (appSession.UsuarioAtivo.IdTipoUsuario == OBC.iPortal.Business.TipoUsuario.OBC && QtdItemsListaDeCCO > 10)
                {
                    if (this.Master is iPortal_User)
                        ((iPortal_User)this.Master).ShowMessage("Alerta - E-Mail n�o enviado", "� permitido apenas 10 endere�os de e-mail no campo 'Com C�pia Oculta', atualmente tem " + ListaDeCCO.ToList().Count().ToString() + ".");
                }
                else
                {
                    sentAll = uiAdmin.EnviarClippings(ListaDeDestinatarios, ListaDeCC, ListaDeCCO, txtFrom.Text, txtSubject.Text, exp.HTML, appSession.UsuarioAtivo.Email, chkMeuEmail.Checked);

                    if (this.Master is iPortal_User && !eVisualizacao && sentAll)
                        ((iPortal_User)this.Master).ShowMessage("Aviso", "Envio de emails conclu�do com sucesso.");
                    else
                        ((iPortal_User)this.Master).ShowMessage("Alerta - E-Mail n�o enviado", "Ocorreram erros no envio de emails.");
                }
            }
            else
            {
                Session["EXPORT.ASPX_MAIL_VISUAL"] = exp.HTML.Replace("<body", "<body style=\"overflow-x:auto;overflow-y:auto;\"");

                //string javaScript = "var minhaJanela = window.open('Email.aspx','winErro','scrollbars=yes,toolbar=yes,titlebar=yes,status=yes,resizable=yes,menubar=yes'); minhaJanela.focus();";

                ScriptManager.RegisterClientScriptBlock(this, typeof(string), "VisualizarEmai", "OpenWindowEmail('Email.aspx', 0)", true);
            }
        }

        protected void ExportXLS(List<Entities.ExportItem> lst)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            OpenXML exp = new OpenXML();

            exp.CreatePackage(ref ms, appSession.ClippingsSelecao.IdClipping_ToExport, Fields, Clippings, Details, Analysis);

            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            //context.Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            context.Response.ContentEncoding = Encoding.Default;
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=OBC_Clippings.xlsx");
            context.Response.AppendHeader("Content-Length", ms.Length.ToString());
            //context.Response.Buffer = true;
            //context.Response.BufferOutput = true;
            context.Response.Flush();
            context.Response.BinaryWrite(ms.GetBuffer());
            context.Response.Flush();
            //context.Response.End();

        }

        /// <summary>
        /// Exporta uma lista de itens para o formato CSV
        /// </summary>
        /// <param name="lst"></param>
        protected void ExportCSV(List<Entities.ExportItem> lst)
        {
            // constr�i um builder que armazenar� todo o conte�do CSV
            StringBuilder sb = new StringBuilder();

            bool f = true;

            // percorre a lista de clippings selecionados
            foreach (int idClipping in appSession.ClippingsSelecao.IdClipping_ToExport)
            {
                // obt�m a lista de colunas que devem ser exportadas
                List<Entities.ExportItem> itens = lst.FindAll(new Predicate<Entities.ExportItem>(delegate (Entities.ExportItem ei)
                {
                    return ei.Codigo == idClipping;
                }));
                // controle de separadores
                bool first = true;
                if (f)
                {
                    foreach (Entities.ExportItem item in itens)
                    {
                        // s� no primeiro item, n�o separa
                        if (!first) sb.Append(";");

                        // preenche os dados formatados
                        sb.Append(item.Desc.Replace(';', '-'));

                        // desabilita controle
                        first = false;
                    }
                    // desabilita controle
                    f = false;
                    // quebra de linha
                    sb.Append("\r\n");
                }

                // verifica se a lista possui itens
                if (itens != null && itens.Count > 0)
                {
                    // ordena a lista de acordo com a ordem pr�-estabelecida
                    itens.Sort(new Comparison<Entities.ExportItem>(delegate (Entities.ExportItem a, Entities.ExportItem b)
                    {
                        return a.Ordem.CompareTo(b.Ordem);
                    }));

                    // controle de separadores
                    first = true;

                    foreach (Entities.ExportItem item in itens)
                    {
                        // s� no primeiro item, n�o separa
                        if (!first) sb.Append(";");

                        // preenche os dados formatados
                        sb.Append(item.ValorTexto.Replace(';', '-'));

                        // desabilita controle
                        first = false;
                    }
                }

                // quebra de linha
                sb.Append("\r\n");
            }


            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.ContentType = "text/csv";
            context.Response.ContentEncoding = Encoding.ASCII;
            context.Response.AppendHeader("Content-Disposition", "attachment; filename=OBC_Clippings.csv");
            context.Response.AppendHeader("Content-Length", sb.ToString().Length.ToString());
            context.Response.Buffer = true;
            context.Response.BufferOutput = true;
            context.Response.Flush();
            context.Response.Write(sb.ToString());
            context.Response.Flush();
            //context.Response.End();
        }
        #endregion

        #endregion

        /* (2011-03-21) fulano - Evento foi para tela ExportExibirClipping */
        /*
#if (VERSIONOLD)

        #region Enum

        private enum MovingTo
        {
            Up,
            Down
        } 

        #endregion

        #region Events

        protected void imgSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveGrupos();

            modalPopupExtenderSelecionados.Hide();
        }

        protected void btnPopSaveOK_Click(object sender, EventArgs e)
        {
            imgSave_Click(sender, new ImageClickEventArgs(0, 0));
        }

        protected void txtGrupo_TextChanged(object sender, EventArgs e)
        {
            int idClip = 0;
            int grupo = 0;
            TextBox txtGrupo = sender as TextBox;

            if (txtGrupo != null)
            {
                if (Int32.TryParse(txtGrupo.ValidationGroup, out idClip) && Int32.TryParse(txtGrupo.Text, out grupo))
                {
                    for (int i = 0; i < Clippings.Count; i++)
                    {
                        if (Clippings[i].IDClipping == idClip)
                        {
                            Clippings[i].Grupo = grupo;
                        }
                    }
                }
            }
            DadosSelecionados = null;
            grdSelecionados.DataSource = DadosSelecionados;
            grdSelecionados.DataBind();

            modalPopupExtenderSelecionados.Show();
        }

        protected void grdSelecionados_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            SaveGrupos();

            List<int> ids = appSession.ClippingsSelecao.IdClipping_ToExport;

            if (e.CommandName.Equals("delete"))
            {
                int indiceSeq = Convert.ToInt32(e.CommandArgument);

                int indiceDados = DadosSelecionados[indiceSeq].RowIndex;

                if (ids.Count >= indiceDados) ids.RemoveAt(indiceDados);

                if (Clippings.Count >= indiceDados)
                {
                    Clippings.RemoveAt(indiceDados);

                    for (int i = 0; i < DadosSelecionados.Count; i++)
                    {
                        if (DadosSelecionados[i].RowIndex >= indiceDados)
                        {
                            DadosSelecionados[i].RowIndex = DadosSelecionados[i].RowIndex - 1;
                        }
                    }
                }

                if (Details.Count >= indiceDados) Details.RemoveAt(indiceDados);
                if (Analysis.Count >= indiceDados) Analysis.RemoveAt(indiceDados);
                if (DadosSelecionados.Count >= indiceSeq - 1)
                {
                    for (int i = indiceSeq; i < DadosSelecionados.Count; i++)
                    {
                        DadosSelecionados[i].Sequencia = DadosSelecionados[i].Sequencia - 1;

                    }
                    DadosSelecionados.RemoveAt(indiceSeq);
                }

                appSession.ClippingsSelecao.IdClipping_ToExport = ids;

                string selecao = String.Empty;
                ids.ForEach(i => selecao = selecao + "," + i.ToString());
                ((iPortal_User)Page.Master).objSelecao.Value = selecao;

                grdSelecionados.DataSource = DadosSelecionados;
                grdSelecionados.DataBind();

                lblQtd.Text = String.Format("Quantidade de clippings: {0}", appSession.ClippingsSelecao.IdClipping_ToExport.Count);

                modalPopupExtenderSelecionados.Show();
            }
        }

        protected void grdSelecionados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void grdSelecionados_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            Label lblCategoria = (Label)e.Row.FindControl("lblCategoria");
            string categoria = lblCategoria.Text;

            Func<Func<int, bool>, bool> hasIndexWhere = (condition) =>
            {
                var result = false;
                for (int indice = 0; indice < DadosSelecionados.Count; indice++)
                {
                    if (!DadosSelecionados[indice].Categoria.Equals(categoria)) continue;

                    if (condition(indice))
                    {
                        result = true;
                        break;
                    }
                }

                return result;
            };

            Func<string, int> getClippingIndex = (commandArgument) =>
            {
                int clippingId;
                if (!Int32.TryParse(commandArgument, out clippingId)) return 0;

                return DadosSelecionados.FindIndex(dado => dado.Categoria.Equals(categoria) && dado.IDClipping == clippingId);
            };


            int clippingIndex = 0;

            ImageButton imgUp = (ImageButton)e.Row.FindControl("btnUp");
            clippingIndex = getClippingIndex(imgUp.CommandArgument);
            imgUp.Visible = hasIndexWhere(currentIndex => currentIndex < clippingIndex);

            ImageButton imgDown = (ImageButton)e.Row.FindControl("btnDown2");
            clippingIndex = getClippingIndex(imgDown.CommandArgument);
            imgDown.Visible = hasIndexWhere(currentIndex => currentIndex > clippingIndex);
        }

        protected void btnDown2_Click(object sender, ImageClickEventArgs e)
        {
            MovingCliptTo(sender as ImageButton, MovingTo.Down);
        }

        protected void btnUp_Click(object sender, ImageClickEventArgs e)
        {
            MovingCliptTo(sender as ImageButton, MovingTo.Up);
        } 

        #endregion

        #region Private Methods
        
        protected void SaveGrupos()
        {
            foreach (GridViewRow row in grdSelecionados.Rows)
            {
                TextBox txtGrupo = row.FindControl("txtGrupo") as TextBox;
                if (txtGrupo == null) continue;

                int nrGrupo = 0;
                int rowIndex = row.RowIndex;
                int clipIndex = rowIndex;

                if (!Int32.TryParse(txtGrupo.Text, out nrGrupo))
                {
                    if (Clippings.Count > clipIndex)
                        Clippings[clipIndex].Grupo = 0;

                    DadosSelecionados[rowIndex].Grupo = string.Empty;
                }
                else
                {
                    if (Clippings.Count > clipIndex)
                        Clippings[clipIndex].Grupo = nrGrupo;

                    DadosSelecionados[rowIndex].Grupo = nrGrupo.ToString();
                }
            }
        }

        private void MovingCliptTo(ImageButton buttonDispatcher, MovingTo movingTo)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "Inicio"));
            SaveGrupos();
            System.Diagnostics.Debug.WriteLine(string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "Pos-SaveGrupos"));

            var clippingId = Convert.ToInt32(buttonDispatcher.CommandArgument);
            var clippingCategory = buttonDispatcher.CommandName;

            System.Diagnostics.Debug.WriteLine(string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "Pre-Find"));
            var clippingIndex = DadosSelecionados.FindIndex(item => item.IDClipping == clippingId && item.Categoria == clippingCategory);
            System.Diagnostics.Debug.WriteLine(string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "Pos-Find"));


            if (movingTo == MovingTo.Up) clippingIndex -= 1;

            DadosSelecionados.Reverse(clippingIndex, 2);
            System.Diagnostics.Debug.WriteLine(string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "Pos-Reverse"));

            grdSelecionados.DataSource = DadosSelecionados;
            grdSelecionados.DataBind();
            System.Diagnostics.Debug.WriteLine(string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "Pos-DataBind"));

            modalPopupExtenderSelecionados.Show();
        }

        #endregion        

#endif
        */
        /* (2011-03-21) fulano - fim */


    }

    public class ItemSelecionado
    {

        public int IDClipping { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public int CategNum { get; set; }
        public string Veiculo { get; set; }
        public string DataCapa { get; set; }
        public string Grupo { get; set; }
        public int RowIndex { get; set; }
        public int Sequencia { get; set; }
        public DateTime Upload { get; set; }
        public int idUpload { get; set; }
        public int Ordem { get; set; }

        public ItemSelecionado(int idClipping, string titulo, string categoria, string veiculo, string dataCapa, int grupo, int index, int categnum, DateTime upload, int idupload, int ordem)
        {
            IDClipping = idClipping;
            Titulo = titulo;
            Categoria = categoria;
            Veiculo = veiculo;
            DataCapa = dataCapa;
            Grupo = grupo > 0 ? grupo.ToString() : String.Empty;
            RowIndex = index;
            CategNum = categnum;
            Upload = upload;
            idUpload = idupload;
            Ordem = ordem;
        }
    }
}
