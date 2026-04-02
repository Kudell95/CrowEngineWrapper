using System.Collections.Generic;
using CosmicCrowGames.Core;
using CrowEngine.Core.Components;
using Microsoft.Xna.Framework;

namespace CrowEngine.Core.Data;
//https://gamedev.stackexchange.com/questions/131133/monogames-most-efficient-way-of-checking-intersections-between-multiple-objects

//possible TODO: abstract this to be a generic quad tree - as this is a quick addition i'll leave it purely for colliders for now.
public class ColliderQuadTree
{
    private const int MAX_OBJECTS = 10;
    private const int MAX_LEVELS = 5;

    private int m_Level;
    private List<Collider2d> m_Objects = new List<Collider2d>();
    private Rectangle m_Bounds;
    private ColliderQuadTree[] m_Nodes;


    public ColliderQuadTree()
    {
        
    }

    public ColliderQuadTree(int level, Rectangle bounds)
    {
        m_Level = level;
        m_Objects = new List<Collider2d>();
        m_Bounds = bounds;
        m_Nodes = new ColliderQuadTree[4];
    }

    public void Clear()
    {
        m_Objects.Clear();

        for (int i = 0; i < m_Level; i++)
        {
            if (m_Nodes[i] != null)
            {
                m_Nodes[i].Clear();
                m_Nodes[i] = null;
            }
        }
    }


    private void Split()
    {
        int subWidth = (int)(m_Bounds.Width / 2);
        int subHeight = (int)(m_Bounds.Height / 2);
        
        int x = (int)(m_Bounds.X / 2);
        int y = (int)(m_Bounds.Y / 2);
        
        m_Nodes[0] = new ColliderQuadTree(m_Level + 1, new Rectangle(x + subWidth,y, subWidth,subHeight));
        m_Nodes[1] = new ColliderQuadTree(m_Level + 1, new Rectangle(x, y, subWidth, subHeight));
        m_Nodes[2] = new ColliderQuadTree(m_Level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
        m_Nodes[3] =  new ColliderQuadTree(m_Level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
    }


    private int GetIndex(Collider2d rect)
    {
        int index = -1;
        double verticalMidpoint = m_Bounds.X + (m_Bounds.Width / 2);
        double horizontalMidpoint = m_Bounds.Y + (m_Bounds.Height / 2);

        float x = rect.Entity.transform.Position.X;
        float y = rect.Entity.transform.Position.Y;
        
        bool topQuadrant = (y < horizontalMidpoint && y + rect.Height < horizontalMidpoint);
        bool bottomQuadrant = (y > horizontalMidpoint);

        if (x < verticalMidpoint && x + rect.Width < verticalMidpoint)
        {
            if (topQuadrant)
                index = 1;
            else if (bottomQuadrant)
                index = 2;
        }
        else if (x > verticalMidpoint)
        {
            if (topQuadrant)
                index = 0;
            else if (bottomQuadrant)
                index = 3;
        }


        return index;

    }

    public void Insert(Collider2d collider)
    {
        if (m_Nodes[0] != null)
        {
            int index = GetIndex(collider);
            if (index != -1)
            {
                m_Nodes[index].Insert(collider);
                return;
            }
        }
        
        
        m_Objects.Add(collider);
        if (m_Objects.Count > MAX_OBJECTS && m_Level < MAX_LEVELS)
        {
            if(m_Nodes[0] == null)
                Split();
            
            int i = 0;
            while (i < m_Objects.Count)
            {
                int index = GetIndex(m_Objects[i]);
                if (index != -1)
                {
                    var col = m_Objects[i];
                    m_Objects.Remove(m_Objects[i]);
                    m_Nodes[index].Insert(m_Objects[i]);
                }
                else
                {
                    i++;
                }
            }
        }


    }

    public List<Collider2d> Retrieve(ref List<Collider2d> returnObjects, Collider2d col)
    {
        if (returnObjects == null)
            returnObjects = new List<Collider2d>();
        
        
        int index = GetIndex(col);
        if (index != -1 && m_Nodes[0] != null)
        {
            m_Nodes[index].Retrieve(ref returnObjects, col);
        }
        
        returnObjects.AddRange(m_Objects);
        
        return returnObjects;
    }
}