   M S B A M L   `   `   `  ����I  EWpfTreinamento, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:WpfTreinamentoWpfTreinamento  5   	       WpfTreinamento.FormTimes   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:WpfTreinamento     5   	     � Title$  Cadastro de Times��5   	   $	��350��6#   $	��800��60    � 
ResizeMode$ NoResize=�6<    � WindowStartupLocation$ CenterScreen=�6R   .��5	      � $��0,0,0,-6q�6   $	��350��6   .+�5
   
   �� -   5         ��Name$ nome_txt��5
      $
��Left=�6?   $��
38,128,0,0q�6Z    �� TextWrapping$
 Wrap=�6n   $	��Top=�6�   $	��258��6�   $��27��6�   $��14(�6�   ��6$   �� *	time.Nome+6�   �� 5   
   -   5        $ div_txt��5      $
��Left=�6A   $��354,128,0,0q�6\   $
 Wrap=�6q   $	��Top=�6�   $��64��6�   $��26��6�   $��14(�6�   ��6#   �� *time.Divisao+6�   �� 5   
   -   5        $ 
regiao_txt��5      $
��Left=�6C   $��488,127,0,0q�6^   $
 Wrap=�6s   $	��Top=�6�   $	��258��6�   $��25��6�   $��14(�6�   ��6&   �� *time.Regiao+6�   �� 5   
   -   5        $ campeonato_txt��5      $
��Left=�6O   $��
38,209,0,0q�6j   $
 Wrap=�6~   $	��Top=�6�   $	��258��6�   $��24��6�   $��14(�6�   ��6*   �� *time.NomeCampeonato+6�   �� 5   
   $
��Left=�6+   $��354,210,0,0q�6F   $
 Wrap=�6[   $	��Top=�6o   $	��154��6�   $��25��6�   ${�False��6�   $��14(�6�   ��6   �� *	time.ID+6�   �� 5   
   $
��Nome��6   $
��Left=�6   $��	38,93,0,0q�6:   $	��Top=�6M   $��35��6e   $	��131��6q   $��18(�6}    J� RenderTransformOrigin$ 0.517,0.173&�6�   �� 5   
   $��Região��6   $
��Left=�6!   $��
488,93,0,0q�6<   $	��Top=�6P   $��34��6h   $	��131��6t   $��18(�6�   �� 5   
   $��Divisão��6   $
��Left=�6"   $��
352,93,0,0q�6=   $	��Top=�6Q   $��35��6i   $	��131��6u   $��18(�6�   �� 5   
   -   5         ��Name$ BtnAdicionar��5      $��Salvar��6�   $
��Left=�6�   $��114,256,0,0q�6�   $	��Top=�6�   $��36��6�   $	��164��6  $��16(�6  $ 0.452,-0.278&�6  ���	#FF07A963
����������	#FF15D895 �� 	ClickMode$ Release=�6�  ��6'   ��  �� RelativeSource �� 	 �� AncestorType"	 ���6�   �� *salvar+6�  �� 5   
   -   5        $ 	BtnEditar��5      $��	Atualizar��6�   $
��Left=�6�   $��114,255,0,0q�6�   $	��Top=�6�   $��36��6�   $	��164��6  $��16(�6  $y�Hidden=�6  ���	#FF07A963���	#FF15D895
���������6$   ��  �� "	 ���6~   �� *editar+6^  �� 5   
   -   5        $ BtnConsultar��5      $��	Consultar��6�   $
��Left=�6�   $��521,256,0,0q�6�   $	��Top=�6�   $��36��6�   $	��162��6
  $��16(�6  $ 0.604,2.039&�6$  ���	#FF07A963
����������	#FF15D895��6'   ��  �� "	 ���6�   �� *abrirTelaMW+6r  �� 5   
   $��ID��6   $
��Left=�6   $��354,175,0,0q�68   $	��Top=�6M   $��35��6e   $	��130��6q   $��18(�6}   �� 5   
   -   5        $ 	BtnLimpar��5      $��Limpar��6�   $
��Left=�6�   $��317,256,0,0q�6�   $	��Top=�6�   $��36��6�   $	��163��6  $��16(�6  ���	#FF07A963
����������	#FF15D895��6$   ��  �� "	 ���6~   �� *limparCampos+6H  �� 5   
   $��Center=�6   $��74��61   
���   �$	��Top=�6L   $	��800��6d   ���	#FF07A9636p   �� 5   
   -	   5        
 ��Name$
 Titulo��5      $��Cadastro de Times��6    $��Center=�6<   $��0,20,0,0q�6Y   $	��Top=�6k   
�������$��18(�6�   $
��Bold"�6�   �� 5   
   $��
Campeonato��6   $
��Left=�6%   $��
38,175,0,0q�6@   $	��Top=�6T   $��34��6l   $	��131��6x   $��18(�6�   5      5      